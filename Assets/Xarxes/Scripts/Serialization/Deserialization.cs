using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserialization : MonoBehaviour
{
    public Dictionary<ACTION_TYPE, Action<SerializedData>> actionsDictionary;
    Serialization2 serialization2 = new Serialization2();

    private void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<SerializedData>>()
        {
            { ACTION_TYPE.SPAWN_OBJECT, SpawnPrefab },
            { ACTION_TYPE.INPUT_PLAYER, ExecuteInput },
            { ACTION_TYPE.DESTROY, Destroy },
        };
    }

    public void SpawnPrefab(SerializedData data)
    {
        Instantiate(Resources.Load(data.parameters as string), new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void ExecuteInput(SerializedData data)
    {
        Debug.Log("ExecuteInput");
    }
    public void Destroy(SerializedData data)
    {
        Debug.Log("Destroy");
    }
}