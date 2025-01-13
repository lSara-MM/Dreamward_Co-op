using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.Rendering.DebugUI.Table;
using System.Net.WebSockets;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Data;
using System.Threading.Tasks;
using TMPro;
using Newtonsoft.Json.Linq;
using System.IO;

public enum BOOLEAN_STATE
{
    NONE = -1,
    FALSE,
    TRUE
}

public class ClientUDP : MonoBehaviour, INetworking
{
    private Socket socket;
    private EndPoint endPoint;

    public InputErrorHandler cs_InputErrorHandler;
    public ChangeScene cs_ChangeScene;
    public string scene = "Hub";

    [SerializeField] private Guid guid;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private BOOLEAN_STATE bs_hostIsValid = BOOLEAN_STATE.NONE;

    Serialization cs_Serialization;

    int recv = 0;

    // Only check if host is valid on the HUB
    // TODO: find a better way to do this
    bool manageHostValid = true;

    private List<(Guid uid, byte[] data, int times)> messageBuffer = new List<(Guid uid, byte[] data, int times)>();
    private readonly object mutex = new object();

    private Dictionary<Guid, DateTime> lastSendTime = new Dictionary<Guid, DateTime>();
    private const int RESEND_INTERVAL = 100; // Milliseconds

    private void Start()
    {
        guid = Guid.NewGuid();
        cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();
    }

    public Guid GetGUID()
    {
        return guid;
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    void Update()
    {
        OnUpdate();

        ResendUnacknowledgedPackets();
    }

    public void InitNetcode()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    public void OnStart()
    {
        playerData = cs_InputErrorHandler.ValidateClient();

        if (playerData != null)
        {
            Debug.Log(guid);

            InitNetcode();

            // Send Ping after receiving

            SerializedData<object> messageData = new SerializedData<object>
            (
                id: guid,
                action: ACTION_TYPE.MESSAGE,
                message: "User Connected Handshake"
            );

            endPoint = new IPEndPoint(IPAddress.Parse(playerData.IP), 9050); // Use Server IP

            // Optional, send handshake
            Globals.StartNewThread(() => SendPacket(messageData, endPoint));

            Globals.StartNewThread(() => ReceiveLoop());
        }
    }

    private void ReceiveLoop()
    {
        byte[] data = new byte[1024];
        EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            try
            {
                recv = socket.ReceiveFrom(data, ref remote);

                if (HostConnected())
                {
                    OnPacketReceived(data, remote);
                    bs_hostIsValid = BOOLEAN_STATE.TRUE;
                }
            }
            catch (SocketException ex)
            {
                //ReportError("Failed to receive packet: " + ex.Message);
                bs_hostIsValid = BOOLEAN_STATE.FALSE;
            }
        }
    }

    public void OnPacketReceived(byte[] inputPacket, EndPoint fromAddress)
    {
        cs_Serialization.DeserializeFromBinary(inputPacket);
        string json = "";

        // Receive Acknowledge
        try
        {
            MemoryStream stream = new MemoryStream(inputPacket);
            BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8);

            json = reader.ReadString();

            json = CleanJson(json); // Remove incomplete JSON data

            if (json != string.Empty) // If no data is recevied don't parse
            {
                var jsonObject = JObject.Parse(json);

                ACTION_TYPE actionType = (ACTION_TYPE)(int)jsonObject["action"];
                Guid packet_id = (Guid)jsonObject["packet_id"];

                if (actionType == ACTION_TYPE.ACKNOWLEDGE)
                {
                    lock (messageBuffer)
                    {
                        messageBuffer.RemoveAll(packet => packet.uid == packet_id);
                        lastSendTime.Remove(packet_id);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to process incoming packet: {ex.Message}");
            Debug.LogError($"Raw JSON: {json}"); // Log the raw JSON content
        }
    }

    private void ResendUnacknowledgedPackets()
    {
        lock (mutex)
        {
            DateTime now = DateTime.UtcNow;

            foreach (var (uid, data, times) in messageBuffer)
            {
                // Check if the resend interval has passed
                if (lastSendTime.ContainsKey(uid))
                {
                    if (times <= 3 && (now - lastSendTime[uid]).TotalMilliseconds > RESEND_INTERVAL)
                    {
                        SendDataPacket(data);
                        lastSendTime[uid] = now; // Update the last send time
                    }
                    else if (times > 3)
                    {
                        messageBuffer.RemoveAll(packet => packet.uid == uid);
                        lastSendTime.Remove(uid);
                    }
                }
            }
            for (int i = 0; i < messageBuffer.Count; i++)
            {
                // Check if the resend interval has passed
                if (lastSendTime.ContainsKey(messageBuffer[i].uid))
                {
                    if (messageBuffer[i].times <= 3 && (now - lastSendTime[messageBuffer[i].uid]).TotalMilliseconds > RESEND_INTERVAL)
                    {
                        SendDataPacket(messageBuffer[i].data);
                        lastSendTime[messageBuffer[i].uid] = now; // Update the last send time
                        int time =  messageBuffer[i].times + 1;
                        messageBuffer[i] = (messageBuffer[i].uid, messageBuffer[i].data, messageBuffer[i].times + 1);
                    }
                    else if (messageBuffer[i].times > 3)
                    {
                        messageBuffer.RemoveAll(packet => packet.uid == messageBuffer[i].uid);
                        lastSendTime.Remove(messageBuffer[i].uid);
                    }
                }
            }
        }
    }

    public static string CleanJson(string json)
    {
        // Iterate from the end to find the point where valid JSON ends
        for (int i = json.Length; i > 0; i--)
        {
            string substring = json.Substring(0, i);

            try
            {
                // Try parsing the substring as JSON
                JObject.Parse(substring);
                // If successful, return this substring as sanitized JSON
                return substring;
            }
            catch
            {
                // Ignore parsing errors, continue truncating
                continue;
            }
        }

        // If no valid JSON is found, return an empty string
        return string.Empty;
    }

    public void OnUpdate()
    {
        if (manageHostValid && playerData != null && bs_hostIsValid != BOOLEAN_STATE.NONE)
        {
            if (bs_hostIsValid == BOOLEAN_STATE.TRUE)
            {
                cs_InputErrorHandler.errorClientGo.SetActive(false);

                Debug.Log("Client Start");
                bs_hostIsValid = BOOLEAN_STATE.NONE;

                Globals.AddDontDestroy(gameObject);

                manageHostValid = false;

                if (cs_ChangeScene)
                {
                    NetDebugKeys.isclient = true;

                    cs_ChangeScene.ChangeToScene(scene);
                }
            }
            else if (bs_hostIsValid == BOOLEAN_STATE.FALSE)
            {
                StartCoroutine(cs_InputErrorHandler.HostMissing());
            }
        }
    }

    public void SendPacket<T>(SerializedData<T> outputPacket, EndPoint toAddress)
    {
        try
        {
            byte[] data = cs_Serialization.SerializeToBinary(outputPacket);

            socket.SendTo(data, toAddress);

            bs_hostIsValid = BOOLEAN_STATE.TRUE;
        }
        catch (SocketException ex)
        {
            ReportError("Failed to send packet: " + ex.Message);
            bs_hostIsValid = BOOLEAN_STATE.FALSE;
        }
    }

    public void SendDataPacket(byte[] data)
    {
        try
        {
            socket.SendTo(data, endPoint);
        }
        catch (SocketException ex)
        {
            ReportError("Failed to send packet: " + ex.Message);
        }
    }

    public async Task SendDataPacketHarshEnvironment(Guid uid, byte[] data, NetConfig config)
    {
        // Add the packet to the message buffer
        messageBuffer.Add((uid, data, 1));
        lastSendTime[uid] = DateTime.UtcNow; // Track the initial send time

        // Copy the message buffer to avoid modifying the original during iteration
        List<(Guid uid, byte[] data, int times)> auxBuffer = new List<(Guid uid, byte[] data, int times)>(messageBuffer);

        System.Random r = new System.Random();

        for (int i = 0; i < auxBuffer.Count; i++)
        {
            DateTime sendTime = DateTime.Now;

            // Determine if packet loss should occur
            bool shouldSendPacket = !config.packetLoss || (r.Next(0, 100) > config.lossThreshold);

            if (shouldSendPacket)
            {
                // Simulate jitter by adding a random delay to the sendTime
                if (config.jitter)
                {
                    sendTime = DateTime.Now.AddMilliseconds(r.Next(config.minJitt, config.maxJitt));
                }

                // Check if jitter is needed and wait asynchronously
                if (DateTime.Now < sendTime)
                {
                    var delayTime = sendTime - DateTime.Now;
                    await Task.Delay(delayTime); // Async delay allows other tasks to run in the meantime
                }

                try
                {
                    // Attempt to send the packet
                    socket.SendTo(auxBuffer[i].data, endPoint);
                }
                catch (SocketException ex)
                {
                    ReportError("Failed to send packet: " + ex.Message);
                }
            }
        }
    }

    public void OnConnectionReset(EndPoint fromAddress)
    {

    }

    public void OnDisconnect()
    {
        socket?.Close();
        socket = null;

        Debug.Log("Client Disconnected");
    }

    public void ReportError(string message)
    {
        Debug.LogError(message);
    }

    public bool HostConnected()
    {
        return recv > 0;
    }

    private void OnApplicationQuit()
    {
        OnDisconnect();
    }
}

