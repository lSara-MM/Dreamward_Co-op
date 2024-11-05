using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ServerUDP : MonoBehaviour
{
    Socket socket;
    public InputErrorHandler cs_InputErrorHandler;
    public ChangeScene cs_ChangeScene;

    public string scene = "Hub";

    [SerializeField] PlayerData playerData;

    void Start()
    {

    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void StartServer()
    {
        playerData = cs_InputErrorHandler.ValidateHost();

        if (playerData != null)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ipep);

            //socket.Listen(10);

            //TO DO 3
            //Our client is sending a handshake, the server has to be able to recieve it
            //It is time to call the Receive thread
            Thread newConnection = new Thread(Receive);
            newConnection.Start();

            Debug.Log("Server Start");
            Globals.AddDontDestroy(gameObject);
            cs_ChangeScene.ChangeToScene(scene);
        }
    }

    void Update()
    {

    }

    void Receive()
    {
        int recv;
        byte[] data = new byte[1024];

        //TO DO 3
        //We don't know who may be comunicating with this server, so we have to create an
        //endpoint with any address and an IpEndpoint from it to reply to it later.
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)(sender);

        //Loop the whole process, and start receiveing messages directed to our socket
        //(the one we binded to a port before)
        //When using socket.ReceiveFrom, be sure send our remote as a reference so we can keep
        //this adress (the client) and reply to it on TO DO 4

        while (true)
        {
            recv = socket.ReceiveFrom(data, ref Remote);

            if (recv == 0)
                break;
            else
            {
                string serverText = "";
                serverText = serverText + "\n" + "Message received from {0}:" + Remote.ToString();
                serverText = serverText + "\n" + Encoding.ASCII.GetString(data, 0, recv);

                Debug.Log(serverText);

            }
            //TO DO 4
            //When our UDP server receives a message from a random remote, it has to send a ping,
            //Call a send thread

            Thread newSend = new Thread(() => Send(Remote));
            newSend.Start();
        }
    }

    void Send(EndPoint Remote)
    {
        //TO DO 4
        //Use socket.SendTo to send a ping using the remote we stored earlier.
        byte[] data = new byte[1024];
        string welcome = "UDP Ping";

        data = Encoding.ASCII.GetBytes(welcome);
        socket.SendTo(data, Remote);
    }
}
