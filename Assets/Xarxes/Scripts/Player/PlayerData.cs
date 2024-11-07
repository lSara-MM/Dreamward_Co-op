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
    public Vector3 color = new Vector3(255, 255, 255);

    public PlayerData(string name, string IP = "0.0.0.0", Vector3 color = default)
    {
        this.network_id = Guid.NewGuid();
        this.name = name;
        this.IP = IP;
        this.color = color; 
    }
}
