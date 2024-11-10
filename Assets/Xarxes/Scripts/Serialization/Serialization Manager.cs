using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SerializationManager
{
    public string StructToJson(SerializedData t)  // Transform the struct into Json
    {
        return JsonUtility.ToJson(t);
    }

    public byte[] SerializeToBinary(SerializedData t)  // Transform the struct into Json and return it serialized in binary
    {
        string json = StructToJson(t);
        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
            {
                writer.Write(json);
            }

            return stream.ToArray();
        }
    }

    public SerializedData DeserializeFromJson(string json)  // Recives Json string and transform it into our Struct
    {
        return JsonUtility.FromJson<SerializedData>(json);
    }

    public SerializedData DeserializeFromBinary(byte[] binaryData)  // Receives the struct in binary and converts it into our struct and returns it
    {
        string json;
        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
            }
        }

        return DeserializeFromJson(json);
    }
}
