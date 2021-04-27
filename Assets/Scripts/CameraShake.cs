using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera vcam;

    public float amp;
    public float freq;

    public float time;

    void Awake()
    {
        vcam = gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    public IEnumerator CamShake()
    {
        Cinemachine.CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = amp;
        noise.m_FrequencyGain = freq;

        yield return new WaitForSeconds(time);

        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;

        yield return 0;
    }
}
