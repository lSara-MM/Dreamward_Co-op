using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using System;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class ServerUDP : MonoBehaviour, INetworking
{
    private Socket socket;
    private EndPoint endPoint;

    [SerializeField] private Guid guid;
    [SerializeField] private PlayerData playerData;
    public InputErrorHandler cs_InputErrorHandler;
    public ChangeScene cs_ChangeScene;
    public string scene = "Hub";

    Serialization cs_Serialization;

    int recv = 0;

    private List<(Guid uid, byte[] data)> packets = new List<(Guid uid, byte[] data)>();
    private float timer = 0f;
    private float timerProcess = 0f;

    private List<Guid> processedPackets = new List<Guid>();

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

    public void StartServer()
    {
        OnStart();
    }

    public void InitNetcode()
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(ipep);
    }

    public void OnStart()
    {
        playerData = cs_InputErrorHandler.ValidateHost();

        if (playerData != null)
        {
            InitNetcode();

            Globals.StartNewThread(() => ReceiveLoop());

            Debug.Log("Server Started");

            Globals.AddDontDestroy(gameObject);
            cs_ChangeScene.ChangeToScene(scene);
        }
        else
        {
            ReportError("Player Data Validation Failed!");
        }
    }

    private void ReceiveLoop()
    {
        byte[] data = new byte[1024];
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        endPoint = sender;

        while (true)
        {
            try
            {
                recv = socket.ReceiveFrom(data, ref endPoint);

                if (ClientConnected())
                {
                    OnPacketReceived(data, endPoint);

                    // Send Ping after receiving

                    SerializedData<object> messageData = new SerializedData<object>
                    (
                        id: guid,
                        action: ACTION_TYPE.MESSAGE,
                        message: "UDP Ping"
                    );

                    Globals.StartNewThread(() => SendPacket(messageData, endPoint));
                }
            }
            catch (SocketException ex)
            {
                ReportError("Socket error: " + ex.Message);
                OnConnectionReset(endPoint);
                break;
            }
        }
    }

    public void OnPacketReceived(byte[] inputPacket, EndPoint fromAddress)
    {
        //if (counter < 2)
        //{
        //    cs_Serialization.DeserializeFromBinary(inputPacket);
        //    counter++;
        //}


        string json = "";

        // Create Acknowledge
        try
        {
            MemoryStream stream = new MemoryStream(inputPacket);
            BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8);

            json = reader.ReadString();

            json = CleanJson(json); // Remove incomplete JSON data

            if (json != string.Empty) // If no data is recevied don't parse
            {
                var jsonObject = JObject.Parse(json);

                Guid packet_id = (Guid)jsonObject["packet_id"];

                packets.Add((packet_id, (byte[])inputPacket.Clone()));

                //Debug.Log(packet_id);

                SerializedData<object> messageData = new SerializedData<object>
                (
                    id: guid,
                    action: ACTION_TYPE.ACKNOWLEDGE,
                    message: "Packet Received Successfully!",
                    packet_id: packet_id
                );

                Globals.StartNewThread(() => SendPacket(messageData, endPoint));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to process incoming packet: {ex.Message}");
            Debug.LogError($"Raw JSON: {json}"); // Log the raw JSON content
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

    private void Update()
    {
        timer += Time.deltaTime;
        timerProcess += Time.deltaTime;

        if (timer >= 0.1f)
        {
            lock (packets)
            {
                for (int i = 0; i < packets.Count; i++)
                {
                    if (!processedPackets.Contains(packets[i].uid))
                    {
                        cs_Serialization.DeserializeFromBinary(packets[i].data);
                        processedPackets.Add(packets[i].uid);

                    }
                }

                packets.Clear();
            }

            timer = 0;
        }

        if (timerProcess >= 5f)
        {
            for (int i = 0; i < processedPackets.Count; i++)
            {
                Debug.Log("ID PROCESSED " + i + " " + processedPackets[i]);
            }

            processedPackets.Clear();
            timerProcess = 0;
        }
    }

    public void OnUpdate()
    {

    }

    public void SendPacket<T>(SerializedData<T> outputPacket, EndPoint toAddress)
    {
        try
        {
            byte[] data = cs_Serialization.SerializeToBinary(outputPacket);

            socket.SendTo(data, toAddress);
        }
        catch (SocketException ex)
        {
            ReportError("Failed to send packet: " + ex.Message);
        }
    }

    public void SendDataPacket(byte[] data)
    {
        if (ClientConnected())
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
    }

    public void OnConnectionReset(EndPoint fromAddress)
    {

    }

    public void OnDisconnect()
    {
        socket.Close();
        Debug.Log("Server Disconnected");
    }

    public void ReportError(string message)
    {
        Debug.LogError(message);
    }

    public bool ClientConnected()
    {
        return recv > 0;
    }

    private void OnApplicationQuit()
    {
        OnDisconnect();
    }
}