using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ACTION_TYPE
{
    SPAWN_PLAYER,
    SPAWN_OBJECT,
    INPUT_PLAYER,
    DESTROY,
    CHANGE_SCENE,
    MESSAGE,
    NONE
}

public interface ISerializedData
{
    Guid network_id { get; }
    ACTION_TYPE action { get; }
    string message { get; }
}

[System.Serializable]
public struct SerializedData<T> : ISerializedData
{
    public Guid network_id { get; set; }
    public ACTION_TYPE action { get; set; }

    // T parameters : where T must be type IDataStructure or basic C# types
    public T parameters { get; set; }
    public string message { get; set; }

    public SerializedData(Guid id, ACTION_TYPE action, T parameters = default, string message = null)
    {
        network_id = id;
        this.action = action;
        this.parameters = parameters;
        this.message = message;
    }
}