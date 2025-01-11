using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

public class UDPJitter : MonoBehaviour
{
    public bool jitter = true;
    public bool packetLoss = true;
    public int minJitt = 0;
    public int maxJitt = 800;
    public int lossThreshold = 90;

    private Socket newSocket; // Added declaration for the socket
    private readonly object myLock = new object(); // Added declaration for the lock object
    private volatile bool exit = false; // Added declaration for the exit flag
    private string myLog = ""; // Added declaration for the log string

    public struct Message
    {
        public byte[] message;
        public DateTime time;
        public uint id;
        public IPEndPoint ip;
    }

    public List<Message> messageBuffer = new List<Message>();

    void Start()
    {
        // Initialize the socket (example with UDP)
        newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Start the sendMessages method in a separate thread
        Thread sendThread = new Thread(SendMessages);
        sendThread.Start();
    }

    void OnDestroy()
    {
        // Ensure threads are properly terminated when the object is destroyed
        exit = true;
        newSocket?.Close();
    }

    void SendMessage(byte[] text, IPEndPoint ip)
    {
        System.Random r = new System.Random();
        if (((r.Next(0, 100) > lossThreshold) && packetLoss) || !packetLoss) // Don't schedule the message with certain probability
        {
            Message m = new Message();
            m.message = text;
            if (jitter)
            {
                m.time = DateTime.Now.AddMilliseconds(r.Next(minJitt, maxJitt)); // Delay the message sending according to parameters
            }
            else
            {
                m.time = DateTime.Now;
            }
            m.id = 0;
            m.ip = ip;
            lock (myLock)
            {
                messageBuffer.Add(m);
            }
            Debug.Log(m.time.ToString());
        }
    }

    // Run this always in a separate Thread to send the delayed messages
    public void SendMessages()
    {
        Debug.Log("Starting to send messages...");
        while (!exit)
        {
            DateTime now = DateTime.Now;
            if (messageBuffer.Count > 0)
            {
                List<Message> auxBuffer;
                lock (myLock)
                {
                    auxBuffer = new List<Message>(messageBuffer);
                }

                int i = 0;
                foreach (var m in auxBuffer)
                {
                    if (m.time <= now)
                    {
                        newSocket.SendTo(m.message, m.message.Length, SocketFlags.None, m.ip);
                        lock (myLock)
                        {
                            messageBuffer.RemoveAt(i);
                        }
                        myLog = Encoding.ASCII.GetString(m.message, 0, m.message.Length);
                        Debug.Log($"Message sent: {myLog}");
                        i--; // Adjust index after removing an item
                    }
                    i++;
                }
            }

            // Sleep briefly to reduce CPU usage in the loop
            Thread.Sleep(10);
        }
    }
}
