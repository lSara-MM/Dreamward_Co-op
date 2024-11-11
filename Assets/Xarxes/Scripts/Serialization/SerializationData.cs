using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ACTION_TYPE
{
    SPAWN_OBJECT,
    INPUT_PLAYER,
    DESTROY,
    MESSAGE
}

[System.Serializable]
public struct SerializedData
{
    public Guid network_id { get; set; }
    public ACTION_TYPE action { get; set; }
    public object parameters { get; set; }
    public string message { get; set; }

    public SerializedData(Guid id, ACTION_TYPE action, object parameters = default, string message = null)
    {
        network_id = id;
        this.action = action;
        this.parameters = parameters;
        this.message = message;
    }
}