using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserialization : MonoBehaviour
{
    public Dictionary<ACTION_TYPE, Action<ISerializedData>> actionsDictionary;

    private Queue<Action> mainThreadActions = new Queue<Action>();

    private void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<ISerializedData>>()
        {
            { ACTION_TYPE.SPAWN_PLAYER, data => QueueActionOnMainThread(() => SpawnPlayer((SerializedData<ns_struct.spawnPlayer>)data)) },
            { ACTION_TYPE.SPAWN_OBJECT, data => QueueActionOnMainThread(() => SpawnPrefab((SerializedData<ns_struct.spawnPrefab>)data)) },
            { ACTION_TYPE.INPUT_PLAYER, data => QueueActionOnMainThread(() => ExecuteInput((SerializedData<ns_struct.playerInput>)data)) },
            { ACTION_TYPE.DESTROY, data => QueueActionOnMainThread(() => Destroy((SerializedData<string>)data)) }
        };
    }

    private void Update()
    {
        // Execute all actions queued for the main thread
        while (mainThreadActions.Count > 0)
        {
            mainThreadActions.Dequeue().Invoke();
        }
    }

    public void QueueActionOnMainThread(Action action)
    {
        mainThreadActions.Enqueue(action);
    }

    public void SpawnPlayer(SerializedData<ns_struct.spawnPlayer> data)
    {
        ns_struct.spawnPlayer param = data.parameters;

        GameObject prefab = Resources.Load(param.path) as GameObject;
        if (prefab != null)
        {
            Instantiate(prefab, param.spawnPosition, Quaternion.identity);

            // Set player data to the received data
            prefab.GetComponent<PlayerOnline>().SetPlayerData(param.playerData);
        }
        else
        {
            Debug.LogError($"Failed to load resource at {param.path}");
        }
    }

    public void SpawnPrefab(SerializedData<ns_struct.spawnPrefab> data)
    {
        ns_struct.spawnPrefab param = data.parameters;

        GameObject prefab = Resources.Load(param.path) as GameObject;
        if (prefab != null)
        {
            Instantiate(prefab, param.spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"Failed to load resource at {param.path}");
        }
    }


    public void ExecuteInput(SerializedData<ns_struct.playerInput> data)
    {
        Debug.Log("ExecuteInput");
    }
    public void Destroy(SerializedData<string> data)
    {
        GameObject objToDestroy = GameObject.Find(data.parameters);
        if (objToDestroy != null)
        {
            Destroy(objToDestroy);
        }
        else
        {
            Debug.LogError($"Object {data.parameters} not found for destruction.");
        }
    }
}