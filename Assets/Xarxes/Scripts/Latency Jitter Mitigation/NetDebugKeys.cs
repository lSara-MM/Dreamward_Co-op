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
        if (Input.GetKeyDown(KeyCode.F7))
        {
            applyNetConfig = !applyNetConfig;
            Debug.Log($"NetConfig applied: {applyNetConfig}");
        }

        HandleDebugKey(KeyCode.F8, "toggle_packet_loss");
        HandleDebugKey(KeyCode.F9, "increase_packet_loss");
        HandleDebugKey(KeyCode.F10, "toggle_jitter");
        HandleDebugKey(KeyCode.F11, "increase_jitter");
    }

    private void HandleDebugKey(KeyCode key, string action)
    {
        if (Input.GetKeyDown(key))
        {
            NetConfig.DebugUpdate(action);
        }
    }
}
