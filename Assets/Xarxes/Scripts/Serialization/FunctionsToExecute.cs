using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionsToExecute : MonoBehaviour
{
    // Link the action types to the funtions to be executed
    public Dictionary<ACTION_TYPE, Action<ISerializedData>> actionsDictionary;

    // Queue all actions to perform so they are made in the main thread
    private Queue<Action> mainThreadActions = new Queue<Action>();

    // Store the GUIDs and their GameObject reference
    public Dictionary<Guid, GameObject> guidDictionary = new Dictionary<Guid, GameObject>();
    
    Serialization cs_Serialization;
    [SerializeField] private GUID_Generator cs_guid;

    private void Start()
    {
        actionsDictionary = new Dictionary<ACTION_TYPE, Action<ISerializedData>>()
        {
            { ACTION_TYPE.SPAWN_PLAYER, data => QueueActionOnMainThread(() => SpawnPlayer((SerializedData<ns_structure.spawnPlayer>)data)) },
            { ACTION_TYPE.SPAWN_OBJECT, data => QueueActionOnMainThread(() => SpawnPrefab((SerializedData<ns_structure.spawnPrefab>)data)) },
            { ACTION_TYPE.INPUT_PLAYER, data => QueueActionOnMainThread(() => ExecuteInput((SerializedData<ns_structure.playerInput>)data)) },
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

    public void SpawnPlayer(SerializedData<ns_structure.spawnPlayer> data)
    {
        ns_structure.spawnPlayer param = data.parameters;

        GameObject prefab = Resources.Load(param.path) as GameObject;
        if (prefab != null)
        {
            cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();

            // There's only one player on Start       
            cs_guid = GameObject.FindGameObjectWithTag("Player").GetComponent<GUID_Generator>();

            GameObject go = Instantiate(prefab, param.spawnPosition, Quaternion.identity);

            // Set player data to the received data
            go.GetComponent<GUID_Generator>().SetGuid(data.network_id);
            guidDictionary.Add(data.network_id, go);

            go.GetComponent<PlayerOnline>().SetPlayerData(param.playerData);
            
            GameObject online;
            if (online = GameObject.FindGameObjectWithTag("Server"))
            {
                PlayerData playerData = online.GetComponent<ServerUDP>().GetPlayerData();

                cs_Serialization.SerializeData(cs_guid.GetGuid(), ACTION_TYPE.SPAWN_PLAYER,
                    new ns_structure.spawnPlayer(playerData, "Player Online NPC", new Vector2(0, 0)));
            }
        }
        else
        {
            Debug.LogError($"Failed to load resource at {param.path}");
        }
    }

    public void SpawnPrefab(SerializedData<ns_structure.spawnPrefab> data)
    {
        ns_structure.spawnPrefab param = data.parameters;

        GameObject prefab = Resources.Load(param.path) as GameObject;
        if (prefab != null)
        {
            GameObject go = Instantiate(prefab, param.spawnPosition, Quaternion.identity);

            GUID_Generator generator = go.GetComponent<GUID_Generator>();
            
            // Save the guid to the map if it has
            if (generator != null)
            {
                go.GetComponent<GUID_Generator>().SetGuid(data.network_id);
                guidDictionary.Add(data.network_id, go);
            }
        }
        else
        {
            Debug.LogError($"Failed to load resource at {param.path}");
        }
    }

    public void ExecuteInput(SerializedData<ns_structure.playerInput> data)
    {
        ns_structure.playerInput param = data.parameters;
        Debug.Log($"Execute Input: {param.key}");

        GameObject go = guidDictionary[data.network_id];

        if (param.key == "Fire1")
        {
            go.GetComponent<PlayerCombat>().CombatMovement(param.key, param.state);
        }
        else
        {
            go.GetComponent<PlayerMovement>().Movement(param.key, param.state);
        }
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