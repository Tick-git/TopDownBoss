using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin _noise;

    private float _timer;

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        _noise = _cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                StopShake();
            }
        }
    }

    public void Shake(float amplitude, float frequency, float duration)
    {
        _noise.m_AmplitudeGain = amplitude;
        _noise.m_FrequencyGain = frequency;

        _timer = duration;
    }

    private void StopShake()
    {
        _noise.m_AmplitudeGain = 0;
        _noise.m_FrequencyGain = 0;
    }
}