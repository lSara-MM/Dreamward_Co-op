using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnline : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    GameObject online;
    Serialization2 cs_Serialization;

    public bool isNPC = false;

    // Start is called before the first frame update
    void Awake()
    {
        DebugCosos();

        cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization2>();

        if (!isNPC)
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
                playerData.SetColorArray(new Color(0.5882353f, 1f, 0.1647059f, 1f));
                Debug.Log("Client");
                Debug.Log("Player: " + playerData.playerNum);

                cs_Serialization.SerializeData(new Guid(), ACTION_TYPE.SPAWN_PLAYER,
                    new ns_struct.spawnPlayer(playerData, "Player Online NPC", new Vector2(0, 0)));
            }
            else
            {
                Debug.Log("Online not found");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key) || Input.GetKeyUp(key) || Input.GetKey(key))
                {
                    cs_Serialization.SerializeData(playerData.netID, ACTION_TYPE.INPUT_PLAYER, key);
                }
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

    public void DebugCosos()
    {
        #region New Serialization Debug with SerializedData spawnPrefab
        //// Provisional Testing 2
        //Serialization2 serialization = new Serialization2();
        //byte[] data = new byte[1024];

        //SerializedData<ns_struct.spawnPrefab> player2 = new SerializedData<ns_struct.spawnPrefab>(new Guid(), ACTION_TYPE.SPAWN_OBJECT,
        //    new ns_struct.spawnPrefab("Player Online NPC.prefab", new Vector2(0, 0)));
        //Debug.Log("Before Serialization: ");
        //player2.parameters.Print();

        //data = serialization.SerializeToBinary(player2);

        //if (serialization.DeserializeFromBinary2(data) is SerializedData<ns_struct.spawnPrefab> emptyStruct)
        //{
        //    //emptyStruct = (SerializedData<ns_struct.spawnPrefab>)serialization.DeserializeFromBinary2(data);
        //    //SerializedData<ns_struct.spawnPrefab> emptyStruct = serialization.DeserializeFromBinary2(data) as SerializedData<ns_struct.spawnPrefab>;
        //    Debug.Log("After Serialization: ");
        //    emptyStruct.parameters.Print();
        //}
        #endregion

        #region New Serialization Debug with SerializedData spawnPrefab
        //// Provisional Testing 2
        //byte[] data = new byte[1024];

        //PlayerData plauerdata = new PlayerData("paquito");
        //plauerdata.playerNum = 2;
        //plauerdata.SetColorArray(new Color(0.5882353f, 1f, 0.1647059f, 1f));

        //SerializedData<ns_struct.spawnPlayer> player2 = new SerializedData<ns_struct.spawnPlayer>(new Guid(), ACTION_TYPE.SPAWN_PLAYER,
        //    new ns_struct.spawnPlayer(plauerdata, "Player Online NPC.prefab", new Vector2(0, 0)));
        //Debug.Log("Before Serialization: ");
        //player2.parameters.Print();

        //data = cs_Serialization.SerializeToBinary(player2);

        //if (cs_Serialization.DeserializeFromBinary2(data) is SerializedData<ns_struct.spawnPlayer> emptyStruct)
        //{
        //    //emptyStruct = (SerializedData<ns_struct.spawnPrefab>)serialization.DeserializeFromBinary2(data);
        //    //SerializedData<ns_struct.spawnPrefab> emptyStruct = serialization.DeserializeFromBinary2(data) as SerializedData<ns_struct.spawnPrefab>;
        //    Debug.Log("After Serialization: ");
        //    emptyStruct.parameters.Print();
        //}
        #endregion
    }
}
