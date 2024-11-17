using System.Net;

public interface INetworking
{
    // Clients and server will have the same UDP interface,
    // the difference will be how they use it.

    // For setup like binding sockets
    void InitNetcode();

    // Called to start the server or client
    void OnStart();

    // Called when a packet is received
    void OnPacketReceived(byte[] inputPacket, EndPoint fromAddress);

    // Called to handle periodic updates, if needed
    void OnUpdate();

    // Sends a packet
    void SendPacket<T>(SerializedData<T> outputPacket, EndPoint toAddress);

    // Called to handle connection resets
    void OnConnectionReset(EndPoint fromAddress);

    // Handle disconnection
    void OnDisconnect();

    // Error handling
    void ReportError(string message);

    // Clean up sockets when closing application
    void CleanUp();
}
