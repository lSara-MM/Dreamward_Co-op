using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnline : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    GameObject online;

    // Start is called before the first frame update
    void Awake()
    {
        if (online = GameObject.FindGameObjectWithTag("Server"))
        {
            playerData = online.GetComponent<ServerUDP>().GetPlayerData();
            playerData.playerNum = 1;
            Debug.Log("Server");
            Debug.Log("Player: " + playerData.playerNum);
        }
        else if (online = GameObject.FindGameObjectWithTag("Client"))
        {
            playerData = online.GetComponent<ClientUDP>().GetPlayerData();
            playerData.playerNum = 2;
            playerData.color = new Color(0.5882353f, 1f, 0.1647059f, 1f);
            Debug.Log("Client");
            Debug.Log("Player: " + playerData.playerNum);
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

    public PlayerData GetPlayerData()
    {
        return playerData;
    }
}
