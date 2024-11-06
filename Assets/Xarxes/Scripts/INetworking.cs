using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public interface INetworking
{
    // Clients and server will have the same UDP interface,
    // the difference will be how they use it.

    void Start();
    void OnPacketReceived(byte[] inputPacket, Socket fromAddress);
    void OnUpdate();
    void OnConnectionReset(Socket fromAddress);
    void SendPacket(byte[] outputPacket, Socket toAddress);
    void OnDisconnect();
    void ReportError();
}
