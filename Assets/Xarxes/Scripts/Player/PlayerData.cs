using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Guid network_id;
    public string name;
    public string IP;

    public int playerNum = 1;
    public Color color = new Color(1f, 1f, 1f, 1f);

    public PlayerData(string name, string IP = "0.0.0.0", Color color = default)
    {
        this.network_id = Guid.NewGuid();
        this.name = name;
        this.IP = IP;
        this.color = color == default ? new Color(1f, 1f, 1f, 1f) : color; // Default is (0,0,0,0) and can't be changed, it has to be done manually.
    }
}