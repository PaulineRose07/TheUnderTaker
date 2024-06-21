using UnityEngine;

public class HeroWeaponBase : ProjectileBase
{
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Invoke("BulletDeactivation", .5f);
    }
    private void Update()
    {
        ProjectileMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
        {
            enemyBase.m_lives -= m_damagesToEnemy;
            enemyBase.TouchedByHeroProjectile();
        }
        if(collision.gameObject.TryGetComponent<NecromancerBehavior>(out NecromancerBehavior behavior))
        {
            behavior.LoseLife(m_damagesToEnemy);
            behavior.TouchedByHeroProjectile();
        }
        if (collision.gameObject.layer == 15)
            return;
        BulletTouchedSomething();
    }

    private void BulletDeactivation()
    {
       gameObject.SetActive(false);
    }
}
