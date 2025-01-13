using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetDebugKeys : MonoBehaviour
{
    static public bool applyNetConfig = false;

    private TextMeshProUGUI networkText;

    static public bool isclient = false;

    private void Awake()
    {
        networkText = GetComponentInChildren<TextMeshProUGUI>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isclient)
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
        if (applyNetConfig && isclient)
        {
            networkText.text = $"Packet Loss: {NetConfig._defaultPacketLoss} | Packet Loss: {NetConfig._lossThresholdSteps[NetConfig._currentLossStepIndex]}% \nJitter: {NetConfig._defaultJitter} | Jitter: {NetConfig._jitterSteps[NetConfig._currentJitterStepIndex]}ms";

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            networkText.text = "";

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false); // Desactivar cada hijo
            }
        }

    }

    private void HandleDebugKey(KeyCode key, string action)
    {
        if (Input.GetKeyDown(key))
        {
            networkText.text = NetConfig.DebugUpdate(action);
        }
    }
}
