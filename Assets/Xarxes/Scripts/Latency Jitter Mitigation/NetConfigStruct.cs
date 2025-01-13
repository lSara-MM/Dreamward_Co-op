using UnityEngine;

public struct NetConfig
{
    public bool jitter;
    public bool packetLoss;

    public int minJitt;
    public int maxJitt;
    public int lossThreshold;

    // Static fields for default configuration
    public static bool _defaultJitter = false;
    public static bool _defaultPacketLoss = false;
    public static int _defaultMinJitt = 0;
    public static int _defaultMaxJitt = 0;

    public static readonly int[] _jitterSteps = { 0, 200, 400, 600, 800 };
    public static readonly int[] _lossThresholdSteps = { 0, 20, 40, 60, 80 };

    public static int _currentJitterStepIndex = 0; // Index for jitter values
    public static int _currentLossStepIndex = 0;  // Index for loss threshold values

    static public string networkDebugText;

    // Method to configure the default values
    public static void ConfigureDefault(bool jitter, bool packetLoss, int minJitt, int maxJitt, int lossThreshold)
    {
        _defaultJitter = jitter;
        _defaultPacketLoss = packetLoss;
        _defaultMinJitt = minJitt;
        _defaultMaxJitt = maxJitt;
        _currentLossStepIndex = 0; // Reset loss threshold to the first step
        _currentJitterStepIndex = 0; // Reset jitter to the first step
    }

    // Apply the current default values
    public static NetConfig ApplyDefault()
    {
        return new NetConfig
        {
            jitter = _defaultJitter,
            packetLoss = _defaultPacketLoss,
            minJitt = _defaultMinJitt,
            maxJitt = _jitterSteps[_currentJitterStepIndex],
            lossThreshold = _lossThresholdSteps[_currentLossStepIndex]
        };
    }

    // Debug method to handle runtime configuration
    public static string DebugUpdate(string action)
    {
        switch (action)
        {
            case "toggle_packet_loss":
                _defaultPacketLoss = !_defaultPacketLoss;
                Debug.Log($"Packet Loss toggled to {_defaultPacketLoss}");
                break;

            case "increase_packet_loss":
                _currentLossStepIndex = (_currentLossStepIndex + 1) % _lossThresholdSteps.Length;
                Debug.Log($"Loss Threshold updated to {_lossThresholdSteps[_currentLossStepIndex]}");
                break;

            case "toggle_jitter":
                _defaultJitter = !_defaultJitter;
                Debug.Log($"Jitter toggled to {_defaultJitter}");
                break;

            case "increase_jitter":
                _currentJitterStepIndex = (_currentJitterStepIndex + 1) % _jitterSteps.Length;
                Debug.Log($"Jitter value updated to {_jitterSteps[_currentJitterStepIndex]}");
                break;
        }

        return "";
    }

}