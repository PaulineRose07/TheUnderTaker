
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float m_shootDelay;
    [SerializeField] PoolSystem m_poolSystem;
    [Header("--- Sounds ---")]
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] List<AudioClip> m_launchShovel;
    private float m_shovelTimer;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        m_shovelTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (m_shovelTimer <= 0)
            {
                GetAWeapon();
                m_shovelTimer = m_shootDelay;
            }
        }
    }

    private void GetAWeapon()
    {
        var shovel = m_poolSystem.GetAvailableShovel();
        shovel.transform.position = transform.position;
        shovel.transform.rotation = transform.rotation;
        shovel.SetActive(true);
        shovel.GetComponent<ProjectileBase>().ActivateSpriteRenderer();
        AudioClip launchingShovel = m_launchShovel[Random.Range(0, m_launchShovel.Count)];
        m_audioSource.PlayOneShot(launchingShovel);
    }

}
