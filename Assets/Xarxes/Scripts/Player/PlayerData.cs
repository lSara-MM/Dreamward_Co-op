using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string name;
    public string IP;

    // Player number by order. Server is always 1
    public int playerNum = 1;
    public float[] color = { 1f, 1f, 1f, 1f };

    public PlayerData(string name = "Player", string IP = "0.0.0.0", Color color = default)
    {
        this.name = name;
        this.IP = IP;

        SetColorArray((color == default ? new Color(1f, 1f, 1f, 1f) : color));
    }

    // Get color in Color type variable (better to handle Unity stuff)
    public Color GetColorColor()
    {
        return new Color(color[0], color[1], color[2], color[3]);
    }

    public void SetColorArray(Color color)
    {
        this.color = new float[] { color.r, color.g, color.b, color.a};
    }
}
