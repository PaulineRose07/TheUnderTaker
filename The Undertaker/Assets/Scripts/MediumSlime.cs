using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumSlime : MonoBehaviour
{

    [SerializeField] public int m_damagesToPlayer;
    [SerializeField] public int m_lives;
    [SerializeField] public int m_pointsToScore;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] float m_speedOfMovement;
    public GameManager m_gameManager;
    public PoolSystem m_poolSystem;
    GameObject m_player;

    private void Start()
    {
        m_player = m_gameManager.m_player;
    }
    // Update is called once per frame
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, m_speedOfMovement * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       //if (collision.gameObject.TryGetComponent<HeroProjectile>(out HeroProjectile projectile))
       if(collision.gameObject.TryGetComponent<HeroProjectile>(out HeroProjectile projectile))
       {
            m_lives -= projectile.m_damagesToEnemy;
            projectile.BulletTouchedSomething();
            if(m_lives == 0)
            {
                StartCoroutine(EnemyDeath());
            }
        }
        if (collision.gameObject.layer == 7)
        {
            m_gameManager.DecreaseLives(m_damagesToPlayer);
        }     
    }

    IEnumerator EnemyDeath()
    {
        m_gameManager.UpdateScore(m_pointsToScore);
        SplitWhenDead();
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);    
        gameObject.SetActive(false);
    }

    private void SplitWhenDead()
    {
        GameObject instanceSplit = m_poolSystem.GetSmallSlime();
        instanceSplit.transform.position = transform.position;
        instanceSplit.SetActive(true);
    }
}
