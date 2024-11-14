using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseLocalIP : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("Missing input Field to get the IP");
            return;
        }
    }

    public void OnButtonClick()
    {
        inputField.text = GetLocalIPAddress();
    }

    string GetLocalIPAddress()
    {
        string localIP = "No IP Found";
        try
        {
            // Loop through all network interfaces on the machine
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Check if the network interface is up and supports IPv4
                if (netInterface.OperationalStatus == OperationalStatus.Up && netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    // Get all the unicast IP addresses associated with this network interface
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        // Filter for IPv4 addresses in the private range (local network)
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string ipString = ip.Address.ToString();
                            
                            // Check if it matches a private IP range (this will give you the local IP address)
                            if (ipString.StartsWith("10.") || ipString.StartsWith("192.168.") || ipString.StartsWith("172."))
                            {
                                localIP = ipString;
                                return localIP;
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error getting the IP: " + e.Message);
        }
        return localIP;
    }
}
