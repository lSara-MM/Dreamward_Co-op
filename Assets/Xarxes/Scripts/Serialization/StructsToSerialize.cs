using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum KEY_STATE
{
    KEY_DOWN,
    KEY_UP,
    KEY_HOLD
}

namespace ns_struct
{
    #region Spawn Object
    public struct spawnPrefab
    {
        public string path { get; set; }
        public Vector2 spawnPosition { get; set; }

        public spawnPrefab(string path, Vector2 spawn)
        {
            this.path = path;
            this.spawnPosition = spawn;
        }

        public void Print()
        {
            Debug.Log($"Path: {path}, SpawnPosition: {spawnPosition}");
        }
    }
    #endregion //Spawn Object

    #region Input

    public struct playerInput
    {
        public KeyCode key { get; set; }
        public KEY_STATE state { get; set; }

        public playerInput(KeyCode key, KEY_STATE state)
        {
            this.key = key;
            this.state = state;
        }

        public void Print()
        {
            Debug.Log($"Key: {key}, State: {state}");
        }
    }

    #endregion //Input
}