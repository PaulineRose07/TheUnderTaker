using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProjectile : MonoBehaviour
{
    [SerializeField] public int m_damagesToEnemy;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private float m_speedOfProjectile = 10f;
    [SerializeField] private float m_bulletLifeSpan = 2f;
    
    private float m_bulletLifeTimer;

    private void Start()
    {
        m_bulletLifeTimer = m_bulletLifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * m_speedOfProjectile);
        m_bulletLifeTimer -= Time.deltaTime;
        if (m_bulletLifeTimer <= 0)
        {
            DeactivateIfLifeSpanIsOver();
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
        {
            enemyBase.TouchedByHeroProjectile(); 
        }
        BulletTouchedSomething();
    }
    public void ActivateSpriteRenderer()
    {
        m_spriteRenderer.enabled = true;
    }

    public void BulletTouchedSomething()
    {
        StartCoroutine(BulletLifeHasEnded());
    }

    IEnumerator BulletLifeHasEnded()
    {
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
        m_bulletLifeTimer = m_bulletLifeSpan;

    }

    private void DeactivateIfLifeSpanIsOver()
    {
        StartCoroutine(BulletLifeHasEnded());
    }
}
