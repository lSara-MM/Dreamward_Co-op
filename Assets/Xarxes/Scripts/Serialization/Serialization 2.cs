using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.Reflection;
using Newtonsoft.Json.Linq;
using ns_struct;
using UnityEditor.ShaderGraph.Serialization;

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
    public Deserialization cs_deserialization;

    private Dictionary<ACTION_TYPE, Action<JObject>> actionsDictionary;

    public void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<JObject>>()
        {
            //{ ACTION_TYPE.SPAWN_OBJECT, data => SpawnPrefab((SerializedData<ns_struct.spawnPrefab>)data) },
            //{ ACTION_TYPE.INPUT_PLAYER, data => SpawnPrefab((SerializedData<ns_struct.spawnPrefab>)data) },
            { ACTION_TYPE.DESTROY, DeserializeDestroy },
        };
    }

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

    public object DeserializeFromBinary2(byte[] binaryData)
    {
        string json;

        using (MemoryStream stream = new MemoryStream(binaryData))
        {
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8))
            {
                json = reader.ReadString();
                var jsonObject = JObject.Parse(json);

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
                    return null;
                }
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

    #region Structs deserialization
    // These methods are the ones the dictionary is storing
    private SerializedData<ns_struct.spawnPrefab> HandleSpawnObject(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.spawnPrefab>();
        var structData = new ns_struct.spawnPrefab();
        structData.Deserialize(jsonObject);
        data.parameters = structData;
        return data;
    }

    private SerializedData<ns_struct.playerInput> HandlePlayerInput(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.playerInput>();
        var structData = new ns_struct.playerInput();
        structData.Deserialize(jsonObject);
        data.parameters = structData;
        return data;
    }

    // Handler for DESTROY action
    private void DeserializeDestroy(JObject jsonObject)
    {
        var data = new SerializedData<ns_struct.spawnPrefab>();
        data.parameters = new ns_struct.spawnPrefab();
        data.parameters.Deserialize(jsonObject);

        cs_deserialization.actionsDictionary[ACTION_TYPE.DESTROY].Invoke(data);
    }
    #endregion // Structs deserialization
}