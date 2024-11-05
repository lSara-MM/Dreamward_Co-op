using System;
using UnityEngine;
using TMPro;

using System.Linq;

public class InputErrorHandler : MonoBehaviour
{
    [Header("Start client - wrong IP/name")]
    public TMP_InputField inputField_IP;
    public TMP_InputField inputField_name;
    public GameObject errorGo;
    public TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerData ValidateClient()
    {
        int ret = 0;

        if (!ValidateName())
        {
            errorGo.SetActive(true);
            errorText.text = "Invalid name";
            ret++;
        }

        if (!ValidateIPv4())
        {
            errorGo.SetActive(true);
            errorText.text = "Invalid IP";
            ret++;
        }

        if (ret == 0)
        {
            errorGo.SetActive(false);
            inputField_IP.gameObject.transform.parent.gameObject.SetActive(false);

            return new PlayerData(inputField_name.text, inputField_IP.text);
        }
        else
        {
            if (ret == 2)
            {
                errorText.text = "Invalid entries";
            }
            return null;
        }
    }

    // Validate the IP the user has introduced. Return true if valid IP
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

    public bool ValidateName()
    {
        return !string.IsNullOrEmpty(inputField_name.text);
    }
}
