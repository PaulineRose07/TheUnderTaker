using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBehavior : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Sprite m_openedGrave;
    [SerializeField] Sprite m_closedGrave;
    [SerializeField] Material m_unlitMaterial;
    [SerializeField] Material m_darkMaterial;
    [SerializeField] Collider2D m_collider2D;


    private void Start()
    {
        m_spriteRenderer.sprite = m_closedGrave;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGraveWhenOpen()
    {
        //m_spriteRenderer.sprite = m_openedGrave;
        m_collider2D.isTrigger = true;
        gameObject.layer = 11;
    }

    public void MaterialChangeToUnlit()
    {
        if (gameObject.layer == 11)
            m_spriteRenderer.sprite = m_openedGrave;
        m_spriteRenderer.material = m_unlitMaterial;
        m_spriteRenderer.sprite = m_openedGrave;
    }

    public void MaterialBackToDark()
    {
        m_spriteRenderer.material = m_darkMaterial;
    }
}
