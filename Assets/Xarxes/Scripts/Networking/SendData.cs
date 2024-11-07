using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour
{
    public delegate void del_SendData();
    public delegate void del_SendInputData(KeyCode key);

    public void SendInput()
    {
        SerializedData serializedData = new SerializedData(new Guid(), ACTION_TYPE.SPAWN_OBJECT);
    }
}
