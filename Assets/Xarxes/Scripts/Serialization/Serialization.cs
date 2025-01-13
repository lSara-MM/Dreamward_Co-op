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
            { ACTION_TYPE.DESTROY, data => HandlePrimitive<string>(data) },
            { ACTION_TYPE.CHANGE_SCENE, data => HandlePrimitive<string>(data) },
            { ACTION_TYPE.BOSS_ATTACK, data => HandlePrimitive<int>(data) },
            { ACTION_TYPE.BOSS_MOVEMENT, data => HandleAction<ns_structure.vector2D>(data) }, 
            { ACTION_TYPE.BOSS_HEALTH, data => HandlePrimitive<int>(data) },
            { ACTION_TYPE.PLAYER_DEATH, data => HandlePrimitive<bool>(data) },
            { ACTION_TYPE.WIN_LOSE, data => HandlePrimitive<bool>(data) },
            //{ ACTION_TYPE.MESSAGE, data => true }, // TO DO
            //{ ACTION_TYPE.ACKNOWLEDGE, data => true } // TO DO
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
            if (NetDebugKeys.applyNetConfig)
            {
                _ = online.GetComponent<ClientUDP>().SendDataPacketHarshEnvironment(serializedData.packet_id, data, NetConfig.ApplyDefault());
            }
            else
            {
                online.GetComponent<ClientUDP>().SendDataPacket(data);
            }
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
    public void DeserializeFromBinary(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();

                try
                {
                    json = CleanJson(json);

                    if (json != string.Empty)
                    {
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
                                //return result;
                            }
                            else
                            {
                                Debug.LogWarning($"Unknown action type: {actionType}");
                            }
                        }

                        //return JsonConvert.DeserializeObject<SerializedData<object>>(json);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception occurred during JSON parsing: {ex.Message}");
                    Debug.LogError($"Raw JSON: {json}"); // Log the raw JSON content
                    throw;
                }
            }
        }
    }

    public static string CleanJson(string json)
    {
        // Iterate from the end to find the point where valid JSON ends
        for (int i = json.Length; i > 0; i--)
        {
            string substring = json.Substring(0, i);

            try
            {
                // Try parsing the substring as JSON
                JObject.Parse(substring);
                // If successful, return this substring as sanitized JSON
                return substring;
            }
            catch
            {
                // Ignore parsing errors, continue truncating
                continue;
            }
        }

        // If no valid JSON is found, return an empty string
        return string.Empty;
    }

    #region Structs deserialization

    // Generic deserialization helper
    private SerializedData<T> HandleAction<T>(JObject jsonObject) where T : ns_structure.IDataStructure, new()
    {
        var data = new SerializedData<T>();

        data.network_id = (Guid)jsonObject["network_id"];
        data.action = (ACTION_TYPE)(int)jsonObject["action"];
        data.parameters = new T(); // Create a new instance of the expected parameter type
        data.parameters.Deserialize(jsonObject); // Call the parameter-specific deserialization

        cs_functionsToExecute.actionsDictionary[data.action].Invoke(data); // Invoke the mapped action
        return data;
    }

    // Action handlers
    private SerializedData<ns_structure.spawnPlayer> HandleSpawnPlayer(JObject jsonObject)
    {
        return HandleAction<ns_structure.spawnPlayer>(jsonObject);
    }

    private SerializedData<ns_structure.spawnPrefab> HandleSpawnObject(JObject jsonObject)
    {
        return HandleAction<ns_structure.spawnPrefab>(jsonObject);
    }

    private SerializedData<ns_structure.playerInput> HandlePlayerInput(JObject jsonObject)
    {
        return HandleAction<ns_structure.playerInput>(jsonObject);
    }

    private SerializedData<T> HandlePrimitive<T>(JObject jsonObject)
    {
        var data = new SerializedData<T>();

        data.network_id = (Guid)jsonObject["network_id"];
        data.action = (ACTION_TYPE)(int)jsonObject["action"];

        // Directly assign the parameter since it's a primitive type
        data.parameters = jsonObject["parameters"].ToObject<T>();

        // Execute the action
        cs_functionsToExecute.actionsDictionary[data.action].Invoke(data);

        return data;
    }
    #endregion // Structs deserialization
}