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
        var receivedData = cs_Serialization.DeserializeFromBinary(inputPacket);

        ISerializedData serializedData = receivedData as ISerializedData;

        //Debug.Log($"Data received from {fromAddress}");

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

    private List<byte[]> messageBuffer = new List<byte[]>();
    private readonly object mutex = new object();

    public void SendDataPacketHarshEnvironment(byte[] data, NetConfig config)
    {
        lock (mutex)
        {
            // Add the packet to the message buffer with the current time
            messageBuffer.Add(data);

            List<byte[]> auxBuffer = new List<byte[]>(messageBuffer);

            System.Random r = new System.Random();

            for (int i = 0; i < auxBuffer.Count; i++)
            {
                DateTime sendTime = DateTime.Now;

                if (((r.Next(0, 100) > config.lossThreshold) && config.packetLoss) || !config.packetLoss) // Don't schedule the message with certain probability
                {
                    if (config.jitter)
                    {
                        sendTime = DateTime.Now.AddMilliseconds(r.Next(config.minJitt, config.maxJitt));
                    }

                    //// Waiting loop to pause until sendTime is reached
                    //while (DateTime.Now <= sendTime)
                    //{
                    //    Thread.Sleep(1); // Sleep for 1 millisecond to avoid a tight loop
                    //}

                    try
                    {
                        socket.SendTo(auxBuffer[i], endPoint);
                    }
                    catch (SocketException ex)
                    {
                        ReportError("Failed to send packet: " + ex.Message);
                    }

                    // Remove the packet from the buffer after sending
                    messageBuffer.RemoveAt(i);
                    i--; // Adjust index after removal
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

