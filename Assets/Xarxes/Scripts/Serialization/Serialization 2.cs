using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

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

public class Serialization2 : MonoBehaviour
{
    public MemoryStream stream;

    public byte[] SerializeToBinary<T>(T data)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

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
