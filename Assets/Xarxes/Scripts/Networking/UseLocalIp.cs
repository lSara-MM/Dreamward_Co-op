using System.Collections;
using System.Collections.Generic;
using System.Net;
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
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
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
