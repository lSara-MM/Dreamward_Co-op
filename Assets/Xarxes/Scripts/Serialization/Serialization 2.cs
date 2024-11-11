using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;

public struct TestStruct
{
    public string name;
    public Vector2 position;
    public object jiji;

    public TestStruct(string name,Vector2 position,object jiji)
    {
        this.name = name;
        this.position = position;
        this.jiji = jiji;
    }
}

public class Serialization2
{
    public MemoryStream stream;
    
    public void SerializeData(Guid id, ACTION_TYPE action, object parameters = default, string message = default)
    {
        SerializedData serializedData = new SerializedData(id, action, parameters, message);
        byte[] data = new byte[1024];

        data = SerializeToBinary(serializedData);

        // TODO: send the data?
    }

    public byte[] SerializeToBinary<T>(T data)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // TODO: dictionary thing

        string json = JsonConvert.SerializeObject(data, settings);

        stream = new MemoryStream();

        using (BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
        {
            writer.Write(json);
        }

        byte[] binaryData = stream.ToArray();

        return binaryData;
    }

    public TestStruct DeserializeFromBinary(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
            }
        }

        return JsonConvert.DeserializeObject<TestStruct>(json);
    }
}
