using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOnline : MonoBehaviour
{
    [SerializeField] private GUID_Generator cs_guid;
    [SerializeField] private PlayerData playerData;

    GameObject online;
    Serialization cs_Serialization;

    // All players have PlayerOnline.cs. If isNPC --> player can't control it
    public bool isNPC = false;

    [SerializeField] private string lastInputType = "";
    [SerializeField] private float lastInputValue = 0f;

    [SerializeField] private SpriteRenderer fadeToBlack;
    [SerializeField] private FollowUI cs_followUI;
    [SerializeField] private GameObject canvasUI;

    // Start is called before the first frame update
    void Awake()
    {
        cs_guid = gameObject.GetComponent<GUID_Generator>();
        cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();

        Globals.AddDontDestroy(gameObject);

        if (!isNPC)
        {
            // Check if server or client
            if (online = GameObject.FindGameObjectWithTag("Server"))
            {
                playerData = online.GetComponent<ServerUDP>().GetPlayerData();
                playerData.playerNum = 1;

                cs_guid.SetGuid(online.GetComponent<ServerUDP>().GetGUID());
            }
            else if (online = GameObject.FindGameObjectWithTag("Client"))
            {
                // Get the playerData from the script. Serialize and send the data to the server

                playerData = online.GetComponent<ClientUDP>().GetPlayerData();
                playerData.playerNum = 2;
                playerData.SetColorArray(new Color(0.5882353f, 1f, 0.1647059f, 1f));

                cs_guid.SetGuid(online.GetComponent<ClientUDP>().GetGUID());

                cs_Serialization.SerializeData(cs_guid.GetGuid(), ACTION_TYPE.SPAWN_PLAYER,
                    new ns_structure.spawnPlayer(playerData, "Player Online NPC", new Vector2(0, 0)));
            }
            else
            {
                Debug.Log("Online not found");
            }
        }
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 6.25f, 0);
        fadeToBlack.color = new Color(0, 0, 0, 0);

        cs_followUI.AssignCam();

        if (canvasUI != null)
        {
            canvasUI.SetActive(!canvasUI.activeInHierarchy);
        }

        GetComponent<Rigidbody2D>().gravityScale = 4.0f;
        GetComponent<PlayerHealth>().AssignPlayerHealth();
        GetComponent<WinLose>().AssignWinLose();
        GetComponent<FadeToBlack>().AssignFadeToBlack();
        GetComponent<Stamina>().AssignStamina();
    }
    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 6.25f, 0);
        fadeToBlack.color = new Color(0, 0, 0, 0);

        cs_followUI.AssignCam();

        if (canvasUI != null)
        {
            canvasUI.SetActive(!canvasUI.activeInHierarchy);
        }

        GetComponent<Rigidbody2D>().gravityScale = 4.0f;
        GetComponent<PlayerHealth>().AssignPlayerHealth();

        if (!isNPC)
        {
            GetComponent<WinLose>().AssignWinLose(online.tag);
        }

        GetComponent<FadeToBlack>().AssignFadeToBlack();
        GetComponent<Stamina>().AssignStamina();
    }

    // Send input data when a valid input is detected
    public void ManageOnlineMovement(string key = default, float key_state = 0, float posX = 0, float posY = 0)
    {
        // Do if 
        //// it's not NPC
        //// current and last key are different / current and last key_state are different / current and last key_state are the same but not 0
        if (!isNPC && (lastInputType != key || lastInputValue != key_state || (lastInputType == key && lastInputValue == key_state && lastInputValue != 0)))
        {
            ns_structure.playerInput playerInput = new ns_structure.playerInput(key, key_state, posX, posY);
            cs_Serialization.SerializeData(cs_guid.GetGuid(), ACTION_TYPE.INPUT_PLAYER, playerInput);
        }
    }
}
