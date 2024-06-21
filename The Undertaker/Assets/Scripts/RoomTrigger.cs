using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] List<DoorBehaviour> m_doors;
    //[SerializeField] DoorBehaviour m_doors;
    [SerializeField] ChamberManager m_chamberManager;
    [SerializeField] GameManager m_gameManager;

    private void Start() {
        m_gameManager = m_chamberManager.m_gameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            foreach(DoorBehaviour door in m_doors)
            {
                door.LockDoors();
            }
            gameObject.SetActive(false);
            m_gameManager.InitializeChamber(m_chamberManager);
        }
    }
}
