using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] DoorBehaviour m_doors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            m_doors.LockDoors();
            gameObject.SetActive(false);
        }
    }
}
