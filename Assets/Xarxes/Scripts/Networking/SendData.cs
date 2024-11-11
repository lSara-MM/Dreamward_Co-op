using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData
{
    public delegate void del_SendData();
    public delegate void del_SendInputData(KeyCode key);
    Serialization2 serialization2 = new Serialization2();


    public void SpawnObject(string prefabName)
    {
        SerializedData serializedData = new SerializedData(new Guid(), ACTION_TYPE.SPAWN_OBJECT, prefabName);
        byte[] data = new byte[1024];

        data = serialization2.SerializeToBinary(serializedData);
        
    }

    public void SendInput()
    {
        SerializedData serializedData = new SerializedData(new Guid(), ACTION_TYPE.INPUT_PLAYER);
    }
}
