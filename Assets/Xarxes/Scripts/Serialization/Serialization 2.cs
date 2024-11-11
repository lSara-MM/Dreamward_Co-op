using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.Reflection;
using Newtonsoft.Json.Linq;

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
    public Deserialization cs_deserialization;

    public void SerializeData<T>(Guid id, ACTION_TYPE action, T parameters = default, string message = default)
    {
        SerializedData<T> serializedData = new SerializedData<T>(id, action, parameters, message);
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
            Debug.Log(json);
        }

        byte[] binaryData = stream.ToArray();

        return binaryData;
    }

    public void DeserializeFromBinary2(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
                ParseData(json);

                //cs_deserialization.actionsDictionary[(ACTION_TYPE)(int)jsonObject["action"]].Invoke(jsonObject);
            }
        }
    }

    private void ParseData(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        ACTION_TYPE action = (ACTION_TYPE)(int)jsonObject["action"];

        // 
        switch (action)
        {
            case ACTION_TYPE.SPAWN_OBJECT:
                {
                    SerializedData<ns_struct.spawnPrefab> data = new SerializedData<ns_struct.spawnPrefab>();
                    ns_struct.spawnPrefab structData = new ns_struct.spawnPrefab();
                    structData.Deserialize(jsonObject);

                    data.parameters = structData;
                    cs_deserialization.actionsDictionary[action].Invoke(data);
                }
                break;
            case ACTION_TYPE.INPUT_PLAYER:
                {
                    SerializedData<ns_struct.playerInput> data = new SerializedData<ns_struct.playerInput>();
                    ns_struct.playerInput structData = new ns_struct.playerInput();
                    structData.Deserialize(jsonObject);

                    data.parameters = structData;
                    cs_deserialization.actionsDictionary[action].Invoke(data);
                }
                break;
            case ACTION_TYPE.DESTROY:
                {
                    SerializedData<ns_struct.spawnPrefab> data = new SerializedData<ns_struct.spawnPrefab>();
                    ns_struct.spawnPrefab structData = new ns_struct.spawnPrefab();
                    structData.Deserialize(jsonObject);

                    data.parameters = structData;
                    cs_deserialization.actionsDictionary[action].Invoke(data);
                }
                break;
            case ACTION_TYPE.MESSAGE:
                {

                }
                break;
        }
    }

    public SerializedData<T> DeserializeFromBinary<T>(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString(); 
                var jsonObject = JObject.Parse(json);
                var name = (string)jsonObject["action"];
            }
        }

        return JsonConvert.DeserializeObject<SerializedData<T>>(json);
    }
}
