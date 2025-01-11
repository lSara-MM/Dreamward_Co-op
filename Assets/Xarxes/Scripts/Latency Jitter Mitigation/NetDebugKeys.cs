using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetDebugKeys : MonoBehaviour
{
    static public bool applyNetConfig = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            applyNetConfig = !applyNetConfig;
        }
    }
}
