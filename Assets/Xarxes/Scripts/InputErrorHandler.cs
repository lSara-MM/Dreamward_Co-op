using System;
using UnityEngine;
using TMPro;

using System.Linq;

public class InputErrorHandler : MonoBehaviour
{
    [Header("Start host - wrong name")]
    public TMP_InputField inputField_HostName;
    public GameObject errorHostGo;

    [Header("Start client - wrong IP/name")]
    public TMP_InputField inputField_IP;
    public TMP_InputField inputField_ClientName;
    public GameObject errorClientGo;
    public TMP_Text errorClientText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Check if the host name is not empty before joining a game
    // Return --> If host is valid: create a player with the info. Else: null
    public PlayerData ValidateHost()
    {
        if (!ValidateName(inputField_HostName.text))
        {
            errorHostGo.SetActive(true);
            return null;
        }
        else
        {
            errorHostGo.SetActive(false);
            return new PlayerData(inputField_HostName.text);
        }
    }

    // Check if all client's fields are correct before joining a game
    // Return --> If client is valid: create a player with the info. Else: null
    public PlayerData ValidateClient()
    {
        int ret = 0;

        if (!ValidateName(inputField_ClientName.text))
        {
            errorClientGo.SetActive(true);
            errorClientText.text = "Invalid name";
            ret++;
        }

        if (!ValidateIPv4())
        {
            errorClientGo.SetActive(true);
            errorClientText.text = "Invalid IP";
            ret++;
        }

        if (ret == 0)
        {
            errorClientGo.SetActive(false);

            return new PlayerData(inputField_ClientName.text, inputField_IP.text);
        }
        else
        {
            if (ret == 2)
            {
                errorClientText.text = "Invalid entries";
            }
            return null;
        }
    }

    // Check if the game to join exists
    public void HostMissing()
    {
        errorClientGo.SetActive(true);
        errorClientText.text = "Host not found";
    }

    // Validate the IP the user has introduced.
    // Return --> If valid IP: true
    public bool ValidateIPv4()
    {
        if (string.IsNullOrEmpty(inputField_IP.text))
        {
            return false;
        }

        string[] splitValues = inputField_IP.text.Split('.');
        if (splitValues.Length != 4)
        {
            return false;
        }

        byte tempForParsing;

        return splitValues.All(r => byte.TryParse(r, out tempForParsing));
    }

    public bool ValidateName(string name)
    {
        return !string.IsNullOrEmpty(name);
    }
}
