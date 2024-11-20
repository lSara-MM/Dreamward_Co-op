using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Serialization : MonoBehaviour
{
    public MemoryStream stream;
    private FunctionsToExecute cs_functionsToExecute;

    // Link the action types to the funtions to be executed
    [SerializeField] private Dictionary<ACTION_TYPE, Func<JObject, object>> actionsDictionary;

    private void Start()
    {
        // Map actions to functions that return a value (SerializedData<T>)
        actionsDictionary = new Dictionary<ACTION_TYPE, Func<JObject, object>>()
        {
            { ACTION_TYPE.SPAWN_PLAYER, data => HandleSpawnPlayer(data) },
            { ACTION_TYPE.SPAWN_OBJECT, data => HandleSpawnObject(data) },
            { ACTION_TYPE.INPUT_PLAYER, data => HandlePlayerInput(data) },
            { ACTION_TYPE.DESTROY, data => HandleDestroy(data) },
        };

        cs_functionsToExecute = gameObject.GetComponent<FunctionsToExecute>();
    }

    // Create and serialize SerializedData<object> and send packed
    // in --> GameObject's GUID
    // in --> Type of action to be performed
    // in (optional) --> SerializedData<object> : where object is type IDataStructure
    // in (optional) --> message to log
    public void SerializeData<T>(Guid id, ACTION_TYPE action, T parameters = default, string message = default)
    {
        SerializedData<T> serializedData = new SerializedData<T>(id, action, parameters, message);
        byte[] data = new byte[1024];

        data = SerializeToBinary(serializedData);

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

    // Convert from SerializedData<object> to Json to binary
    // Return --> Data in bytes[]
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
            //Debug.Log(json);
        }

        byte[] binaryData = stream.ToArray();

        return binaryData;
    }

    // Deserialize data and call the functions to update the game's state
    // Return --> If success: Deserialized data. Else: default empty SerializedData<object>
    public object DeserializeFromBinary(byte[] binaryData)
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
                    ACTION_TYPE actionType = (ACTION_TYPE)(int)jsonObject["action"];
                    if (actionsDictionary.ContainsKey(actionType))
                    {
                        var action = actionsDictionary[actionType];

                        // Call the delegate
                        var result = action.Invoke(jsonObject);

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

    #region Structs deserialization
    private SerializedData<ns_structure.spawnPlayer> HandleSpawnPlayer(JObject jsonObject)
    {
        var data = new SerializedData<ns_structure.spawnPlayer>();

        data.network_id = (Guid)jsonObject["network_id"];

        data.parameters = new ns_structure.spawnPlayer();
        data.parameters.Deserialize(jsonObject);

        cs_functionsToExecute.actionsDictionary[ACTION_TYPE.SPAWN_PLAYER].Invoke(data);

        return data;
    }

    private SerializedData<ns_structure.spawnPrefab> HandleSpawnObject(JObject jsonObject)
    {
        var data = new SerializedData<ns_structure.spawnPrefab>();
        
        data.network_id = (Guid)jsonObject["network_id"];
        
        data.parameters = new ns_structure.spawnPrefab();
        data.parameters.Deserialize(jsonObject);

        cs_functionsToExecute.actionsDictionary[ACTION_TYPE.SPAWN_OBJECT].Invoke(data);
        return data;
    }

    private SerializedData<ns_structure.playerInput> HandlePlayerInput(JObject jsonObject)
    {
        var data = new SerializedData<ns_structure.playerInput>();

        data.network_id = (Guid)jsonObject["network_id"];

        data.parameters = new ns_structure.playerInput();
        data.parameters.Deserialize(jsonObject);

        cs_functionsToExecute.actionsDictionary[ACTION_TYPE.INPUT_PLAYER].Invoke(data);
        return data;
    }

    private SerializedData<string> HandleDestroy(JObject jsonObject)
    {
        var data = new SerializedData<string>();

        data.network_id = (Guid)jsonObject["network_id"];

        cs_functionsToExecute.actionsDictionary[ACTION_TYPE.DESTROY].Invoke(data);
        return data;
    }
    #endregion // Structs deserialization
}