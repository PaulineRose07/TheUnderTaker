using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] DoorBehaviour m_doors;
    [SerializeField] public int m_roomIdInGMList;
    [SerializeField] ChamberManager m_chamberManager;
    [SerializeField] GameManager m_gameManager;

    private void Start() {
        m_gameManager = m_chamberManager.m_gameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            if(m_chamberManager.m_hasAlreadyBeenVisited == false) 
            {
                m_doors.LockDoors();
                gameObject.SetActive(false);
                m_gameManager.ChangeChambers(m_gameManager.m_chamberManagers[m_roomIdInGMList]);
            }
        }
    }
}
