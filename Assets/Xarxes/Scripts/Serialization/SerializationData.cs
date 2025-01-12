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
    DESTROY,    // needs string
    CHANGE_SCENE,    // needs string
    BOSS_ATTACK,    // needs int
    BOSS_MOVEMENT,    // needs vector2D
    BOSS_HEALTH,    // needs int 
    PLAYER_DEATH,
    WIN_LOSE,    // needs bool
    MESSAGE,
    ACKNOWLEDGE,
    NONE
}

public interface ISerializedData
{
    Guid network_id { get; }
    ACTION_TYPE action { get; }
    string message { get; }
    public Guid packet_id { get; }
}

[System.Serializable]
public struct SerializedData<T> : ISerializedData
{
    public Guid network_id { get; set; }

    public ACTION_TYPE action { get; set; }

    // T parameters : where T must be type IDataStructure or basic C# types
    public T parameters { get; set; }

    public string message { get; set; }

    public Guid packet_id { get; set; }

    public SerializedData(Guid id, ACTION_TYPE action, T parameters = default, string message = null, Guid packet_id = default)
    {
        network_id = id;
        this.action = action;
        this.parameters = parameters;
        this.message = message;
        this.packet_id = packet_id == Guid.Empty ? Guid.NewGuid() : packet_id;
    }
}