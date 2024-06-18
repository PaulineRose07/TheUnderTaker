using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public GameManager m_gameManager;
    public PoolSystem m_poolSystem;
    public GameObject m_player;
    public int m_damagesToPlayer;
    public int m_lives;
    public int m_pointsToScore;
    public float m_speedOfMovement;
    public SpriteRenderer m_spriteRenderer;
    public Collider2D m_collider2D;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.TryGetComponent<ProjectileBase>(out ProjectileBase projectile))
        {
            //m_lives -= projectile.m_damagesToEnemy;
            //TouchedByHeroProjectile();
        }*/
        if (collision.gameObject.layer == 7)
        {
            m_gameManager.DecreaseLives(m_damagesToPlayer);
        }

    }

    abstract public void TouchedByHeroProjectile();

    public void ShowYourself()
    {
        m_spriteRenderer.enabled = true;
    }

    public void HideYourself()
    {
        m_spriteRenderer.enabled = false;
    }
}
