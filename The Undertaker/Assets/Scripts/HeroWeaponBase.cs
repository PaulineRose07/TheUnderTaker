using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeaponBase : ProjectileBase
{
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
        {
            enemyBase.m_lives -= m_damagesToEnemy;
            enemyBase.TouchedByHeroProjectile();
        }
        BulletTouchedSomething();
    }
}
