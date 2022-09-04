using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _amplitudeGain;
    [SerializeField] private float _frequencyGainh;
    [SerializeField] private float _duration;

    private CinemachineVirtualCamera _vcam;
    private CinemachineBasicMultiChannelPerlin _noise;
    
    public void StartShake()
    {
        StartCoroutine(Shake(_duration));
    }

    private void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _noise = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Noise(float amplitudeGain, float frequencyGain)
    {
        _noise.m_AmplitudeGain = amplitudeGain;
        _noise.m_FrequencyGain = frequencyGain;
    }

    private IEnumerator Shake(float duration)
    {
        Noise(_amplitudeGain, _frequencyGainh);
        yield return new WaitForSeconds(duration);
        Noise(0f, 0f);
    }
}
