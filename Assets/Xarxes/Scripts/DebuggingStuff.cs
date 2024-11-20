using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingStuff : MonoBehaviour
{
    public Serialization cs_Serialization;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            DebugSerialization();
        }
    }

    public void DebugSerialization()
    {
        #region New Serialization Debug with SerializedData spawnPlayer
        // Provisional Testing 2
        byte[] data = new byte[1024];

        PlayerData plauerdata = new PlayerData("paquito");
        plauerdata.playerNum = 2;
        plauerdata.SetColorArray(new Color(0.5882353f, 1f, 0.1647059f, 1f));

        SerializedData<ns_structure.spawnPlayer> player2 = new SerializedData<ns_structure.spawnPlayer>(new Guid(), ACTION_TYPE.SPAWN_PLAYER,
            new ns_structure.spawnPlayer(plauerdata, "Player Online NPC.prefab", new Vector2(0, 0)));
        Debug.Log("Before Serialization: ");
        player2.parameters.Print();

        data = cs_Serialization.SerializeToBinary(player2);

        if (cs_Serialization.DeserializeFromBinary(data) is SerializedData<ns_structure.spawnPlayer> emptyStruct)
        {
            Debug.Log("After Serialization: ");
            emptyStruct.parameters.Print();
        }
        #endregion
    }
}
