using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Collider2D m_collider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockDoor()
    {
        m_spriteRenderer.color = Color.white;
        m_collider.isTrigger = true;

    }
}
