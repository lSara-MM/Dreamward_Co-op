using Newtonsoft.Json.Linq;
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
    public interface IDataStruct
    {
        public void Print();
        public void Deserialize(JObject jsonObject);
    }

    #region Spawn Object
    public struct spawnPrefab : IDataStruct
    {
        public string path { get; set; }
        public Vector2 spawnPosition { get; set; }

        public spawnPrefab(string path = default, Vector2 spawn = default)
        {
            this.path = path;
            this.spawnPosition = spawn;
        }

        public void Print()
        {
            Debug.Log($"Path: {path}, SpawnPosition: {spawnPosition}");
        }

        public void Deserialize(JObject jsonObject)
        {
            this.path = (string)jsonObject["path"];
            this.spawnPosition.Set((float)jsonObject["spawnPosition"], (float)jsonObject["spawnPosition"]);
        }
    }
    #endregion //Spawn Object

    #region Input

    public struct playerInput : IDataStruct
    {
        public KeyCode key { get; set; }
        public KEY_STATE state { get; set; }

        public playerInput(KeyCode key = default, KEY_STATE state = default)
        {
            this.key = key;
            this.state = state;
        }

        public void Print()
        {
            Debug.Log($"Key: {key}, State: {state}");
        }

        public void Deserialize(JObject jsonObject)
        {
            this.key = (KeyCode)(int)jsonObject["key"];
            this.state = (KEY_STATE)(int)jsonObject["state"];
        }
    }

    #endregion //Input
}