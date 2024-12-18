using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GUID_Generator cs_guid;

    [SerializeField] private string serverPrefabPath;
    [SerializeField] private Vector3 spawnServerPosition;

    [SerializeField] private string clientPrefabPath;
    [SerializeField] private Vector3 clientServerPosition;

    GameObject online;
    Serialization cs_Serialization;

    void Awake()
    {
        cs_Serialization = GameObject.FindGameObjectWithTag("Serialization").GetComponent<Serialization>();

        // Check if server or client
        if (online = GameObject.FindGameObjectWithTag("Server"))
        {
            GameObject prefab = Resources.Load(serverPrefabPath) as GameObject;

            GameObject go = Instantiate(prefab, spawnServerPosition, Quaternion.identity);
            cs_guid = go.GetComponent<GUID_Generator>();

            cs_Serialization.SerializeData((cs_guid != null ? cs_guid.GetGuid() : default), ACTION_TYPE.SPAWN_OBJECT,
                new ns_structure.spawnPrefab(clientPrefabPath, clientServerPosition));
        }
        else if (online = GameObject.FindGameObjectWithTag("Client"))
        {
            // TODO: Add client functionalities at start of the boss
        }
        else
        {
            Debug.Log("Online not found");
        }

        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
