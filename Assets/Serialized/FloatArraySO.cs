using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FloatArraySO : ScriptableObject
{
    [SerializeField] private float[] _values;

    public float[] Value
    {
        get { return _values; }
        set { _values = value; }
    }
}
