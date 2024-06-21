using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionBase : MonoBehaviour
{
    public GameObject m_player;
    public GameManager m_gameManager;
    public List<AudioClip> m_potionTaking;
    public AudioSource m_audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovements>(out PlayerMovements playerMovements))
        {
            AddPowerUpToPlayer(playerMovements);
            AudioClip potionTaking = m_potionTaking[Random.Range(0, m_potionTaking.Count)];
            m_audioSource.PlayOneShot(potionTaking);
            DeactivateWhenTouched();
        }
    }

    public abstract void AddPowerUpToPlayer(PlayerMovements playerMovements);

    private void DeactivateWhenTouched()
    {
        gameObject.SetActive(false);
    }
}
