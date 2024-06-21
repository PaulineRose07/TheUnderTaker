using UnityEngine;

public class RoomTriggerAlways : MonoBehaviour
{

    [SerializeField] DoorBehaviour m_doors;
    [SerializeField] ChamberManager m_chamberManager;
    [SerializeField] GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = m_chamberManager.m_gameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            m_gameManager.SwitchCamera(m_chamberManager);
        }
    }
}
