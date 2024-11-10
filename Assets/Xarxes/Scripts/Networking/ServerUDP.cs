using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ServerUDP : MonoBehaviour, INetworking
{
    private Socket socket;
    [SerializeField] private PlayerData playerData;
    public InputErrorHandler cs_InputErrorHandler;
    public ChangeScene cs_ChangeScene;
    public string scene = "Hub";

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
        EndPoint remote = sender;

        while (true)
        {
            try
            {
                int recv = socket.ReceiveFrom(data, ref remote);

                if (recv > 0)
                {
                    OnPacketReceived(data, remote);

                    // Send Ping after receiving

                    SerializedData messageData = new SerializedData
                    (
                        id: GetPlayerData().network_id,
                        action: ACTION_TYPE.MESSAGE,
                        message: "UDP Ping"
                    );

                    Globals.StartNewThread(() => SendPacket(messageData, remote));
                }
            }
            catch (SocketException ex)
            {
                ReportError("Socket error: " + ex.Message);
                OnConnectionReset(remote);
                break;
            }
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
        // TODO
    }

    public void SendPacket(SerializedData outputPacket, EndPoint toAddress)
    {
        byte[] data = SerializationManager.SerializeToBinary(outputPacket);

        try
        {
            socket.SendTo(data, toAddress);
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
        socket.Close();
        Debug.Log("Server Disconnected");
    }

    public void ReportError(string message)
    {
        Debug.LogError(message);
    }
}

//void Receive()
//{
//    int recv;
//    byte[] data = new byte[1024];

//    //TO DO 3
//    //We don't know who may be comunicating with this server, so we have to create an
//    //endpoint with any address and an IpEndpoint from it to reply to it later.
//    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
//    EndPoint Remote = sender;

//    //Loop the whole process, and start receiveing messages directed to our socket
//    //(the one we binded to a port before)
//    //When using socket.ReceiveFrom, be sure send our remote as a reference so we can keep
//    //this adress (the client) and reply to it on TO DO 4

//    while (true)
//    {
//        recv = socket.ReceiveFrom(data, ref Remote);

//        if (recv == 0)
//            break;
//        else
//        {
//            string serverText = "";
//            serverText = serverText + "\n" + "Message received from {0}:" + Remote.ToString();
//            serverText = serverText + "\n" + Encoding.ASCII.GetString(data, 0, recv);

//            Debug.Log(serverText);

//        }
//        //TO DO 4
//        //When our UDP server receives a message from a random remote, it has to send a ping,
//        //Call a send thread

//        Globals.StartNewThread(() => Send(Remote));
//    }
//}

//void Send(EndPoint Remote)
//{
//    //TO DO 4
//    //Use socket.SendTo to send a ping using the remote we stored earlier.
//    byte[] data = new byte[1024];
//    string welcome = "UDP Ping";

//    data = Encoding.ASCII.GetBytes(welcome);
//    socket.SendTo(data, Remote);
//}