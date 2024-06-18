using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public SpriteRenderer m_spriteRenderer;
    public float m_speedOfProjectile = 10f;
    public int m_damagesToEnemy;
    public Collider2D m_collider;


    // Update is called once per frame
    void Update()
    {
        ProjectileMovement();
    }

    public void ProjectileMovement()
    {
        transform.Translate(Vector2.up * Time.deltaTime * m_speedOfProjectile);
    }

    public void BulletTouchedSomething()
    {
        StartCoroutine(BulletLifeHasEnded());
    }

    IEnumerator BulletLifeHasEnded()
    {
        m_collider.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
    }

    public void ActivateSpriteRenderer()
    {
        m_spriteRenderer.enabled = true;
        m_collider.enabled = true;
    }
}
