using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingStuff : MonoBehaviour
{
    public Serialization serialization;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            DebugCosos();
        }
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

        #region New Serialization Debug with SerializedData spawnPlayer
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
