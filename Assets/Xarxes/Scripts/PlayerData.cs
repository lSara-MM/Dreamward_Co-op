using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Guid network_id;
    public string name;
    public string IP;

    public PlayerData(string name, string IP = "0.0.0.0")
    {
        this.network_id = Guid.NewGuid();
        this.name = name;
        this.IP = IP;
    }
}
