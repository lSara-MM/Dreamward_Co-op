using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUID_Generator : MonoBehaviour
{
    [SerializeField] Guid guid = Guid.NewGuid();

    public Guid GetGuid()
    {
        return guid;
    }

    public void SetGuid(Guid g)
    {
        guid = g;
    }
}