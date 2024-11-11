using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnline : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    GameObject online;
    Serialization2 cs_Serialization = new Serialization2();

    // Start is called before the first frame update
    void Awake()
    {
        #region Old Serialization Debug
        // Provisional Testing
        //SerializedData serializedData = new SerializedData(new Guid(), ACTION_TYPE.MESSAGE, parameters: new Vector2(10, 15));
        //Debug.Log("Before Serialization: " + serializedData.network_id + " / " + serializedData.action + " / " + serializedData.parameters);

        //byte[] data = SerializationManager.SerializeToBinary(serializedData);
        //string hexString = BitConverter.ToString(data).Replace("-", " ");
        //Debug.Log("Datos binarios en hexadecimal: " + hexString);

        //SerializedData serializedDataReply = SerializationManager.DeserializeFromBinary(data);
        //Debug.Log("After Serialization: " + serializedDataReply.network_id + " / " + serializedDataReply.action + " / " + serializedDataReply.parameters); 
        #endregion

        #region New Serialization Debug
        // Provisional Testing 2
        Serialization2 serialization2 = new Serialization2();
        byte[] data = new byte[1024];
        TestStruct player = new TestStruct("Juan", new Vector2(4000, 30), new Vector2(978654, 8765));
        Debug.Log("Before Serialization: " + player.name + " / " + player.position + " / " + player.jiji);

        data = serialization2.SerializeToBinary(player);
        TestStruct emptyStruct = new TestStruct();

        emptyStruct = serialization2.DeserializeFromBinary(data);
        Debug.Log("After Serialization: " + emptyStruct.name + " / " + emptyStruct.position + " / " + emptyStruct.jiji);
        #endregion

        #region New Serialization Debug with SerializedData
        //// Provisional Testing 2
        //Serialization2 serialization = new Serialization2();
        //byte[] data = new byte[1024];
        //SerializedData player2 = new SerializedData(new Guid(), ACTION_TYPE.SPAWN_OBJECT, new Vector2(4000, 30));
        //Debug.Log("Before Serialization: " + player2.network_id + " / " + player2.action + " / " + player2.parameters);

        //data = serialization.SerializeToBinary(player2);
        //SerializedData emptyStruct = new SerializedData();

        //emptyStruct = serialization.DeserializeFromBinary(data);
        //Debug.Log("After Serialization: " + emptyStruct.network_id + " / " + emptyStruct.action + " / " + emptyStruct.parameters);
        #endregion

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


            // TODO: TESTING STUFF
            //SerializedData serializedData = new SerializedData(new Guid(), ACTION_TYPE.SPAWN_OBJECT, playerData);
            
            //SerializationManager serializationManager = new SerializationManager();
            //byte[] data = serializationManager.SerializeToBinary(serializedData);
            //string hexString = BitConverter.ToString(data).Replace("-", " ");
            //Debug.Log("Datos binarios en hexadecimal: " + hexString);

            //SerializedData serializedDataReply = serializationManager.DeserializeFromBinary(data);
            //Debug.Log(serializedDataReply.network_id + serializedDataReply.action.ToString());
        }
        else
        {
            Debug.Log("Online not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key) || Input.GetKeyUp(key))
                {
                    // TODO: should serialize the data to send it?
                    cs_Serialization.SerializeData(playerData.network_id, ACTION_TYPE.INPUT_PLAYER, key);
                }
            }
        }
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }
}
