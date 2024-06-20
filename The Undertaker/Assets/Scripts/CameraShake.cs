using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera m_cinemachineVirtualCamera;
    [SerializeField] private float m_shakeTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        m_cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        enabled = false;
    }
   
    public void ShakeCamera(float _intensity, float _time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineMulti = 
        m_cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineMulti.m_AmplitudeGain = _intensity;
        
        m_shakeTimer = _time;
    }

    public void CameraEnabled() {
        enabled = true;
    }

    public void CameraDisabled() { enabled = false;
    }
    private void Update()
    {
        if(m_shakeTimer > 0)
        {
            m_shakeTimer -= Time.deltaTime; 
            if(m_shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineMulti =
                m_cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineMulti.m_AmplitudeGain = 0f;
            }
        }
    }
}
