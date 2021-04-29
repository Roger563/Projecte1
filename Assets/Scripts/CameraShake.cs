using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera vcam;
    private Death death;

    public float amp;
    public float freq;

    public float time;
    private float respTime;

    private bool canEnter = true;

    void Awake()
    {
        vcam = gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        death = GameObject.FindGameObjectWithTag("Player").GetComponent<Death>();
    }

    void Update()
    {
        if(death.dead == true && canEnter)
        {
            canEnter = false;
            StartCoroutine("CamShake");
        }
    }

    public IEnumerator CamShake()
    {
        Cinemachine.CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = amp;
        noise.m_FrequencyGain = freq;

        yield return new WaitForSeconds(time);

        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;

        yield return new WaitForSeconds(death.OriginalRespawnTimer - time);
        canEnter = true;

        yield return 0;
    }
}
