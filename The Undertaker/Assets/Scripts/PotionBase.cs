using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionBase : MonoBehaviour
{
    public GameObject m_player;
    public GameManager m_gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovements>(out PlayerMovements playerMovements))
        {
            AddPowerUpToPlayer();
            DeactivateWhenTouched();
        }
    }

    public abstract void AddPowerUpToPlayer();

    private void DeactivateWhenTouched()
    {
        gameObject.SetActive(false);
    }
}
