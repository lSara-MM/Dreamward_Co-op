using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingStuff : MonoBehaviour
{
    public Serialization2 serialization;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            DebugCosos();
        }
    }

    public void DebugCosos()
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
        //// Provisional Testing 2
        //Serialization2 serialization2 = new Serialization2();
        //byte[] data = new byte[1024];
        //TestStruct player = new TestStruct("Juan", new Vector2(4000, 30), new Vector2(978654, 8765));
        //Debug.Log("Before Serialization: " + player.name + " / " + player.position + " / " + player.jiji);

        //data = serialization2.SerializeToBinary(player);
        //TestStruct emptyStruct = new TestStruct();

        //emptyStruct = serialization2.DeserializeFromBinary(data);
        //Debug.Log("After Serialization: " + emptyStruct.name + " / " + emptyStruct.position + " / " + emptyStruct.jiji);
        #endregion

        #region New Serialization Debug with SerializedData
        //// Provisional Testing 2
        //Serialization2 serialization = new Serialization2();
        //byte[] data = new byte[1024];
        //SerializedData<Vector2> player2 = new SerializedData<Vector2>(new Guid(), ACTION_TYPE.DESTROY, new Vector2(87654, 30));
        //Debug.Log("Before Serialization: " + player2.network_id + " / " + player2.action + " / " + player2.parameters);

        //data = serialization.SerializeToBinary(player2);
        //SerializedData<Vector2> emptyStruct = new SerializedData<Vector2>();

        //emptyStruct = serialization.DeserializeFromBinary<Vector2>(data);
        //Debug.Log("After Serialization: " + emptyStruct.network_id + " / " + emptyStruct.action + " / " + emptyStruct.parameters);
        #endregion

        #region New Serialization Debug with SerializedData
        //// Provisional Testing 2
        //Serialization2 serialization = new Serialization2();
        //byte[] data = new byte[1024];

        //SerializedData<ns_struct.playerInput> player2 = new SerializedData<ns_struct.playerInput>(new Guid(), ACTION_TYPE.SPAWN_OBJECT, new ns_struct.playerInput(KeyCode.N, KEY_STATE.KEY_UP));
        //Debug.Log("Before Serialization: ");
        //player2.parameters.Print();

        //data = serialization.SerializeToBinary(player2);
        //SerializedData<ns_struct.playerInput> emptyStruct = serialization.DeserializeFromBinary<ns_struct.playerInput>(data);
        //Debug.Log("After Serialization: ");
        //emptyStruct.parameters.Print();
        #endregion

        #region New Serialization Debug with SerializedData spawnPrefab
        // Provisional Testing 2
        byte[] data = new byte[1024];

        SerializedData<ns_struct.spawnPrefab> player2 = new SerializedData<ns_struct.spawnPrefab>(new Guid(), ACTION_TYPE.SPAWN_OBJECT,
            new ns_struct.spawnPrefab("Player Online NPC", new Vector2(0, 0)));
        Debug.Log("Before Serialization: ");
        player2.parameters.Print();

        data = serialization.SerializeToBinary(player2);

        if (serialization.DeserializeFromBinary2(data) is SerializedData<ns_struct.spawnPrefab> emptyStruct)
        {
            //emptyStruct = (SerializedData<ns_struct.spawnPrefab>)serialization.DeserializeFromBinary2(data);
            //SerializedData<ns_struct.spawnPrefab> emptyStruct = serialization.DeserializeFromBinary2(data) as SerializedData<ns_struct.spawnPrefab>;
            Debug.Log("After Serialization: ");
            emptyStruct.parameters.Print();
        }
        #endregion
    }
}
