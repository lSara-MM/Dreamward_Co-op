using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnline : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    GameObject online;

    // Start is called before the first frame update
    void Start()
    {
        if (online = GameObject.FindGameObjectWithTag("Server"))
        {
            playerData = online.GetComponent<ServerUDP>().GetPlayerData();
        }
        else if (online = GameObject.FindGameObjectWithTag("Client"))
        {
            playerData = online.GetComponent<ClientUDP>().GetPlayerData();
        }
        else
        {
            Debug.Log("Online not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
