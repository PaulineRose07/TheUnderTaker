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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HeroProjectile>(out HeroProjectile projectile))
        {
            m_lives -= projectile.m_damagesToEnemy;
            projectile.BulletTouchedSomething();
            TouchedByHeroProjectile();
        }
        if (collision.gameObject.layer == 7)
        {
            m_gameManager.UpdateLives(m_damagesToPlayer);
        }

    }

    abstract public void TouchedByHeroProjectile();

}
