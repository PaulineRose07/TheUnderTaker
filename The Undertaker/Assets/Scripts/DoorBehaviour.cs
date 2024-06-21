
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Sprite m_doorOpen;
    [SerializeField] Sprite m_doorClose;
    [SerializeField] Collider2D m_collider;
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_unlockdoors;
    [SerializeField] AudioClip m_lockDoors;
    
    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer.sprite = m_doorOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockDoors()
    {
        m_audioSource.PlayOneShot(m_lockDoors);
        m_spriteRenderer.sprite = m_doorClose;
        m_collider.isTrigger = false;
    }
    public void UnlockDoor()
    {
        m_audioSource.PlayOneShot(m_unlockdoors);
        m_spriteRenderer.sprite = m_doorOpen;
        m_collider.isTrigger = true;
    }
}
