using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public struct testStruct
{
    public string name;
    public Vector2 position;
}

public class SerializationManager : MonoBehaviour
{
    public MemoryStream stream;

    public string StructToJson(testStruct t)  // Transform the struct into Json
    {
        return JsonUtility.ToJson(t);
    }

    public byte[] SerializeToBinary(testStruct t)  // Transform the struct into Json and return it serialized in binary
    {
        string json = JsonUtility.ToJson(t);
        stream = new MemoryStream();
        using (BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
        {
            writer.Write(json);
        }

        return stream.ToArray();
    }

    public testStruct DeserializeFromJson(string json)  // Recives Json string and transform it into our Struct
    {
        return JsonUtility.FromJson<testStruct>(json);
    }

    public testStruct DeserializeFromBinary(byte[] binaryData)  // Receives the struct in binary and converts it into our struct and returns it
    {
        string json;
        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
            }
        }

        return JsonUtility.FromJson<testStruct>(json);
    }
}
