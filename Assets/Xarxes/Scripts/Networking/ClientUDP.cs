using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Runtime.CompilerServices;
using UnityEngine.tvOS;

public enum BOOLEAN_STATE
{
    NONE = -1,
    FALSE,
    TRUE
}

public class ClientUDP : MonoBehaviour, INetworking
{
    private Socket socket;
    public InputErrorHandler cs_InputErrorHandler;
    public ChangeScene cs_ChangeScene;
    public string scene = "Hub";

    [SerializeField] private PlayerData playerData;
    [SerializeField] private BOOLEAN_STATE bs_changeScene = BOOLEAN_STATE.NONE;

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void StartClient()
    {
        OnStart();
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
            Debug.Log(playerData.network_id.ToString());

            InitNetcode();

            // Send Ping after receiving

            SerializedData messageData = new SerializedData
            (
                id: GetPlayerData().network_id,
                action: ACTION_TYPE.MESSAGE,
                message: "User Connected Handshake"
            );

            EndPoint clientEndPoint = new IPEndPoint(IPAddress.Parse(playerData.IP), 9050); // Use Server IP

            Globals.StartNewThread(() => SendPacket(messageData, clientEndPoint));
        }
    }

    private void Receive()
    {
        byte[] data = new byte[1024];
        EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            int recv = socket.ReceiveFrom(data, ref remote);

            if (recv > 0)
            {
                OnPacketReceived(data, remote);
                bs_changeScene = BOOLEAN_STATE.TRUE;
            }
        }
        catch (SocketException ex)
        {
            ReportError("Failed to receive packet: " + ex.Message);
            bs_changeScene = BOOLEAN_STATE.FALSE;
        }
    }

    public void OnPacketReceived(byte[] inputPacket, EndPoint fromAddress)
    {
        SerializedData receivedData = SerializationManager.DeserializeFromBinary(inputPacket);

        Debug.Log($"Data received from {fromAddress}");

        if (!string.IsNullOrEmpty(receivedData.message))
        {
            Debug.Log($"Message received: {receivedData.message}");
        }
    }

    public void OnUpdate()
    {
        if (playerData != null)
        {
            if (bs_changeScene == BOOLEAN_STATE.TRUE)
            {
                Debug.Log("Client Start");
                bs_changeScene = BOOLEAN_STATE.NONE;

                Globals.AddDontDestroy(gameObject);
                cs_ChangeScene.ChangeToScene(scene);
            }
            else if (bs_changeScene == BOOLEAN_STATE.FALSE)
            {
                cs_InputErrorHandler.HostMissing();
            }
        }
    }

    public void SendPacket(SerializedData outputPacket, EndPoint toAddress)
    {
        try
        {
            byte[] data = SerializationManager.SerializeToBinary(outputPacket);

            socket.SendTo(data, toAddress);

            // Start receive thread to listen for responses
            Globals.StartNewThread(Receive);
        }
        catch (SocketException ex)
        {
            ReportError("Failed to send packet: " + ex.Message);
        }
    }

    public void OnConnectionReset(EndPoint fromAddress)
    {
        // TODO
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

    //void Send(byte[] data)
    //{
    //    //TO DO 2
    //    //Unlike with TCP, we don't "connect" first,
    //    //we are going to send a message to establish our communication so we need an endpoint
    //    //We need the server's IP and the port we've binded it to before
    //    //Again, initialize the socket
    //    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(playerData.IP), 9050);

    //    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    //    //TO DO 2.1 
    //    //Send the Handshake to the server's endpoint.
    //    //This time, our UDP socket doesn't have it, so we have to pass it
    //    //as a parameter on it's SendTo() method

    //    socket.SendTo(data, ipep);

    //    //TO DO 5
    //    //We'll wait for a server response,
    //    //so you can already start the receive thread
    //    Thread receive = new Thread(Receive);
    //    receive.Start();
    //}

    //TO DO 5
    //Same as in the server, in this case the remote is a bit useless
    //since we already know it's the server who's communicating with us
    //void Receive()
    //{
    //    //IPEndPoint sender;
    //    //EndPoint Remote;

    //    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
    //    EndPoint Remote = (EndPoint)(sender);

    //    byte[] data = new byte[1024];

    //    int recv = 0;

    //    try
    //    {
    //        recv = socket.ReceiveFrom(data, ref Remote);
    //        bs_changeScene = BOOLEAN_STATE.TRUE;
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogWarning(ex, this);
    //        bs_changeScene = BOOLEAN_STATE.FALSE;
    //        throw;
    //    }

    //    //clientText = ("Message received from {0}: " + Remote.ToString());
    //    //clientText = clientText += "\n" + Encoding.ASCII.GetString(data, 0, recv);
    //}
}

