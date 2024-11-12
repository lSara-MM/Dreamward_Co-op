using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public struct TestStruct
{
    public string name;
    public Vector2 position;
    public object jiji;

    public TestStruct(string name, Vector2 position, object jiji)
    {
        this.name = name;
        this.position = position;
        this.jiji = jiji;
    }
}

public class Serialization2 : MonoBehaviour
{
    public MemoryStream stream;
    public Deserialization cs_deserialization;

    [SerializeField] private Dictionary<ACTION_TYPE, Action<JObject>> actionsDictionary;

    private void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<JObject>>()
        {
            { ACTION_TYPE.SPAWN_PLAYER, data => HandleSpawnPlayer(data) },
            { ACTION_TYPE.SPAWN_OBJECT, data => HandleSpawnObject(data) },
            { ACTION_TYPE.INPUT_PLAYER, data => HandlePlayerInput(data) },
            { ACTION_TYPE.DESTROY, data => HandleSpawnObject(data) },
        };
    }

    public void SerializeData<T>(Guid id, ACTION_TYPE action, T parameters = default, string message = default)
    {
        SerializedData<T> serializedData = new SerializedData<T>(id, action, parameters, message);
        byte[] data = new byte[1024];

        data = SerializeToBinary(serializedData);

        // TODO: send the data?
        GameObject online;

        if (online = GameObject.FindGameObjectWithTag("Server"))
        {
            online.GetComponent<ServerUDP>().SendDataPacket(data);
        }
        else if (online = GameObject.FindGameObjectWithTag("Client"))
        {
            online.GetComponent<ClientUDP>().SendDataPacket(data);
        }
        else
        {
            Debug.Log("Online not found");
        }
    }

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
            Debug.Log(json);
        }

        byte[] binaryData = stream.ToArray();

        return binaryData;
    }

    public object DeserializeFromBinary2(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
                var jsonObject = JObject.Parse(json);

                if (jsonObject.ContainsKey("action"))
                {
                    //return ParseData(json);

                    ACTION_TYPE actionType = (ACTION_TYPE)(int)jsonObject["action"];
                    if (actionsDictionary.ContainsKey(actionType))
                    {
                        var action = actionsDictionary[actionType];

                        // Here we call the delegate (Func<JObject, SerializedData<T>>) to get the right type T
                        var result = action.DynamicInvoke(jsonObject);

                        // Return the deserialized data
                        return result;
                    }
                    else
                    {
                        Debug.LogWarning($"Unknown action type: {actionType}");

                        JsonConvert.DeserializeObject<SerializedData<object>>(json);
                    }
                }

                return JsonConvert.DeserializeObject<SerializedData<object>>(json);
            }
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

    private object ParseData(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        ACTION_TYPE action = (ACTION_TYPE)(int)jsonObject["action"];

        // 
        switch (action)
        {
            case ACTION_TYPE.SPAWN_OBJECT:
                {
                    return HandleSpawnObject(jsonObject);
                }
            case ACTION_TYPE.INPUT_PLAYER:
                {
                    return HandlePlayerInput(jsonObject);
                }
            case ACTION_TYPE.DESTROY:
                {
                    return HandleSpawnObject(jsonObject);
                }
            case ACTION_TYPE.MESSAGE:
                {
                    return HandleSpawnObject(jsonObject);
                }
        }

        return null;
    }

    #region Structs deserialization
    private SerializedData<ns_struct.spawnPlayer> HandleSpawnPlayer(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.spawnPlayer>();
        data.parameters = new ns_struct.spawnPlayer();
        data.parameters.Deserialize(jsonObject);

        cs_deserialization.actionsDictionary[ACTION_TYPE.SPAWN_PLAYER].Invoke(data);
        return data;
    }

    private SerializedData<ns_struct.spawnPrefab> HandleSpawnObject(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.spawnPrefab>();
        data.parameters = new ns_struct.spawnPrefab();
        data.parameters.Deserialize(jsonObject);

        cs_deserialization.actionsDictionary[ACTION_TYPE.SPAWN_OBJECT].Invoke(data);
        return data;
    }

    private SerializedData<ns_struct.playerInput> HandlePlayerInput(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.playerInput>();
        data.parameters = new ns_struct.playerInput();
        data.parameters.Deserialize(jsonObject);

        cs_deserialization.actionsDictionary[ACTION_TYPE.INPUT_PLAYER].Invoke(data);
        return data;
    }

    private SerializedData<string> DeserializeDestroy(JObject jsonObject)
    {
        var data = new SerializedData<string>();
        //data.parameters.Deserialize(jsonObject);

        cs_deserialization.actionsDictionary[ACTION_TYPE.DESTROY].Invoke(data);
        return data;
    }
    #endregion // Structs deserialization
}