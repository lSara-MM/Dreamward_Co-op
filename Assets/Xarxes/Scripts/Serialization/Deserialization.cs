using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserialization : MonoBehaviour
{
    public Dictionary<ACTION_TYPE, Action<ISerializedData>> actionsDictionary;

    private void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<ISerializedData>>()
        {
            { ACTION_TYPE.SPAWN_OBJECT, data => SpawnPrefab((SerializedData<ns_struct.spawnPrefab>)data) },
            { ACTION_TYPE.INPUT_PLAYER, data => ExecuteInput((SerializedData<ns_struct.playerInput>)data) },
            { ACTION_TYPE.DESTROY, data => Destroy((SerializedData<string>)data) }
        };
    }

    public void SpawnPrefab(SerializedData<ns_struct.spawnPrefab> data)
    {
        ns_struct.spawnPrefab param = data.parameters;
        Instantiate(Resources.Load(param.path), param.spawnPosition, Quaternion.identity);
    }

    public void ExecuteInput(SerializedData<ns_struct.playerInput> data)
    {
        Debug.Log("ExecuteInput");
    }
    public void Destroy(SerializedData<string> data)
    {
        Debug.Log("Destroy");
        Destroy(GameObject.Find(data.parameters));
    }
}