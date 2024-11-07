using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct SerializedData
{
    public Guid network_id { get; set; }
    public ACTION_TYPE action { get; set; }
    public object parameters { get; set; }

    public SerializedData(Guid id, ACTION_TYPE action, object parameters = default)
    {
        network_id = id;
        this.action = action;
        this.parameters = parameters;
    }
}

public enum ACTION_TYPE
{
    SPAWN_OBJECT,
    INPUT_PLAYER,
    DESTROY
}

public class Serialization
{
    public Dictionary<ACTION_TYPE, Action<string>> actionsDictionary /*= new Dictionary<ACTION_TYPE, Action<string>>
    {
        { ACTION_TYPE.SPAWN_OBJECT, },
    }*/;

    //public string StructToJson<T>(T t) where T : struct  // Transform the struct into Json
    //{
    //    return JsonUtility.ToJson(t);
    //}

    //public byte[] SerializeToBinary<T>(T t) where T : struct  // Transform the struct into Json and return it serialized in binary
    //{
    //    string json = JsonUtility.ToJson(t);
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        using (BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
    //        {
    //            writer.Write(json);
    //        }

    //        return stream.ToArray();
    //    }
    //}

    //public T DeserializeFromJson<T>(string json) where T : struct // Recives Json string and transform it into our Struct
    //{
    //    return JsonUtility.FromJson<T>(json);
    //}

    //public T DeserializeFromBinary<T>(byte[] binaryData) where T : struct // Receives the struct in binary and converts it into our struct and returns it
    //{
    //    string json;
    //    using (MemoryStream stream = new MemoryStream(binaryData))
    //    {
    //        using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
    //        {
    //            json = reader.ReadString();
    //        }
    //    }

    //    return JsonUtility.FromJson<T>(json);
    //}
}