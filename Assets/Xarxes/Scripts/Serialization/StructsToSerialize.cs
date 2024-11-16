using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;

public enum KEY_STATE
{
    KEY_DOWN,
    KEY_UP,
    KEY_HOLD,
    NONE
}

namespace ns_struct
{
    public interface IDataStruct
    {
        public void Print();
        public void Deserialize(JObject jsonObject);
    }

    #region Spawn Object
    public class spawnPlayer : IDataStruct
    {
        public PlayerData playerData;
        public string path { get; set; }
        public Vector2 spawnPosition { get; set; }

        public spawnPlayer(PlayerData playerData = null, string path = default, Vector2 spawn = default)
        {
            this.playerData = playerData == null ? new PlayerData() : playerData;
            this.path = path;
            this.spawnPosition = spawn;
        }

        public void Print()
        {
            Debug.Log($"Player: {playerData.name}, Path: {path}, SpawnPosition: {spawnPosition}");
        }

        public void Deserialize(JObject jsonObject)
        {
            // Prevent null reference errors.
            if (jsonObject["parameters"]?["playerData"] != null)
            {
                JObject playerData = (JObject)jsonObject["parameters"]["playerData"];

                // Deserialize playerData name and IP, if they exist
                this.playerData.name = (string)playerData["name"] ?? "Player";
                this.playerData.IP = (string)playerData["IP"] ?? "";

                // Deserialize color array
                JArray colorArray = (JArray)playerData["color"];
                if (colorArray != null && this.playerData.color.Length >= colorArray.Count)
                {
                    for (int i = 0; i < colorArray.Count; i++)
                    {
                        this.playerData.color[i] = (float)colorArray[i];
                    }
                }
            }
            else
            {
                // Log an error if playerData is missing
               Debug.LogWarning("PlayerData not found in JSON.");
            }

            // 
            this.path = (string)jsonObject["parameters"]["path"];
            this.spawnPosition.Set((float)jsonObject["parameters"]["spawnPosition"]["x"], (float)jsonObject["parameters"]["spawnPosition"]["y"]);
        }
    }

    public class spawnPrefab : IDataStruct
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
            this.path = (string)jsonObject["parameters"]["path"];
            this.spawnPosition.Set((float)jsonObject["parameters"]["spawnPosition"]["x"], (float)jsonObject["parameters"]["spawnPosition"]["y"]);
        }
    }
    #endregion //Spawn Object

    #region Input

    public class playerInput : IDataStruct
    {
        public string key { get; set; }

        public float state { get; set; }

        public playerInput(string key = default, float state = default)
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
            this.key = (string)jsonObject["parameters"]?["key"];
            this.state = (float)jsonObject["parameters"]?["state"];
        }
    }

    #endregion //Input
}