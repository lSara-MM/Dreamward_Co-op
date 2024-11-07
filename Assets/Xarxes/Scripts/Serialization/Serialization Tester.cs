using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializationTester : MonoBehaviour
{
    SerializationManager serializationManager;
    byte[] data;

    void Start()
    {
        serializationManager = new SerializationManager();

        data = new byte[1024];
        testStruct player = new testStruct();
        player.name = "Juan";
        player.position = new Vector2(this.transform.position.x, this.transform.position.y);

        testStruct emptyStruct = new testStruct();

        data = serializationManager.SerializeToBinary(player);
        string hexString = BitConverter.ToString(data).Replace("-", " ");
        Debug.Log("Datos binarios en hexadecimal: " + hexString);

        emptyStruct = serializationManager.DeserializeFromBinary(data);
        Debug.Log(emptyStruct.name + emptyStruct.position.ToString());
    }

    void Update()
    {
        
    }
}