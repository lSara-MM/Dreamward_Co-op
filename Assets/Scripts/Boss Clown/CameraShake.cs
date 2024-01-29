using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private float _timer = 0f;
    private float _shakeTime;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    private float shakeIntensity;

    void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cbmcp = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _cbmcp.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0, 1 - (_timer / _shakeTime));
        }
    }

    public void ShakeCamera(float intensity, float frequency, float time)
    {
        _cbmcp.m_AmplitudeGain = intensity;
        _cbmcp.m_FrequencyGain = frequency;
        shakeIntensity = intensity;
        _shakeTime = time;
        _timer = time;
    }
}
