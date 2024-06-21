using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] NecromancerBehavior m_necromancer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            m_necromancer.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
