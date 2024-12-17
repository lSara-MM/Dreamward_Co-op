using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace ns_structure
{
    // Base data Interface that all data structures inherit from
    public interface IDataStructure
    {
        public void Print();
        public void Deserialize(JObject jsonObject);
    }

    public class vector2D : IDataStructure
    {
        public float x;
        public float y;

        public vector2D()
        {
            x = 0;
            y = 0;
        }

        public vector2D(float x, float y)
        {
            this.x = 0;
            this.y = 0;
        }

        public void Print()
        {
            Debug.Log($"X: {x}, Y: {y}");
        }

        public void Deserialize(JObject jsonObject)
        {
            x = (float)jsonObject["parameters"]?["x"];
            y = (float)jsonObject["parameters"]?["y"];
        }
    }

    #region Spawn Objects
    public class spawnPlayer : IDataStructure
    {
        public PlayerData playerData;

        // Path from the /Resource folder
        public string path { get; set; }

        public Vector2 spawnPosition { get; set; }

        public spawnPlayer()
        {
            playerData = new PlayerData();
            path = "";
            spawnPosition = default;
        }

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
           
            Print();
        }
    }

    public class spawnPrefab : IDataStructure
    {
        // Path from the /Resource folder
        public string path { get; set; }

        public Vector2 spawnPosition { get; set; }
        public spawnPrefab()
        {
            path = "";
            spawnPosition = default;
        }

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
            path = (string)jsonObject["parameters"]["path"];
            spawnPosition.Set((float)jsonObject["parameters"]["spawnPosition"]["x"], (float)jsonObject["parameters"]["spawnPosition"]["y"]);

            Print();
        }
    }
    #endregion //Spawn Objects

    #region Input

    public class playerInput : IDataStructure
    {
        // Name of the input in the Input manager (old)
        public string key { get; set; }

        // Ranges from -1 to 1. Manage movement axis and key/button states
        public float state { get; set; }

        // Player position to fix it if there is latency
        public float posX { get; set; }

        public float posY { get; set; }
        public playerInput()
        {
            key = "";
            state = default;
            posX = default;
            posY = default;
        }

        public playerInput(string key = default, float state = default, float posX = default, float posY = default)
        {
            this.key = key;
            this.state = state;
            this.posX = posX;
            this.posY = posY;
        }

        public void Print()
        {
            Debug.Log($"Key: {key}, State: {state}, PosX: {posX}, PosY: {posY}");
        }

        public void Deserialize(JObject jsonObject)
        {
            key = (string)jsonObject["parameters"]?["key"];
            state = (float)jsonObject["parameters"]?["state"];
            posX = (float)jsonObject["parameters"]?["posX"];
            posY = (float)jsonObject["parameters"]?["posY"];
        }
    }

    #endregion //Input
}