using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyBase
{
    [SerializeField] private float m_FleeCount = 2f;
    [SerializeField] private float m_fireDelay = 2f;
    [SerializeField] private bool m_isShooting;
    [SerializeField] private float m_directionDelay = 1f;
    private float m_timerChangeDirection;
    private float m_fireTimer;
    private float m_timerFlee;

    private void Start() 
    {
        m_spriteRenderer.enabled = false;
        m_isShooting = true;
    }

    public override void TouchedByHeroProjectile() {
        if (m_lives <= 0) {
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath() {
        m_gameManager.UpdateScore(m_pointsToScore);
        m_gameManager.m_amountOfSpawns--;
        //AudioClip explosionClip = m_clipListExplosion[Random.Range(0, m_clipListExplosion.Count)];
        //m_audioSource.PlayOneShot(explosionClip);
        m_collider2D.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
   

    // Update is called once per frame
    void Update()
    {
        MoveWhenSeen();
        if (m_player.transform.position.x > transform.position.x) {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;
    }

    public new void OnTriggerReaction()
    {
        m_isShooting = false;
        m_timerFlee = m_FleeCount;

    }

   
    private void MoveWhenSeen()
    {
        if (m_isShooting) return;
        m_timerFlee -= Time.deltaTime;
        if(m_timerFlee <= 0)
        {
            Vector3 randomDirection = new Vector3();
            m_timerChangeDirection -= Time.deltaTime;
            if(m_timerChangeDirection <= 0)
            {
                float m_directionX = Random.Range(-1f, 1f);
                float m_directionY = Random.Range(-1f, 1f);
                randomDirection = new Vector3(m_directionX, m_directionY,0).normalized;

                m_timerChangeDirection = m_directionDelay;
            }
            transform.Translate(randomDirection * m_speedOfMovement, Space.World);
        }
    }

    public new void OnTriggerExitReaction()
    {
        m_isShooting = true;
        
    }

    private void ShootWhenHidden()
    {
        if (!m_isShooting) return;
        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0)
        {
            ThrowABone();
            m_fireTimer = m_fireDelay;
        }
    }

    private void ThrowABone()
    {
        /*var shovel = m_poolSystem.GetAvailableShovel();
        shovel.transform.position = transform.position;
        shovel.transform.rotation = transform.rotation;
        shovel.SetActive(true);
        shovel.GetComponent<ProjectileBase>().ActivateSpriteRenderer();
        AudioClip launchingShovel = m_launchShovel[Random.Range(0, m_launchShovel.Count)];
        m_audioSource.PlayOneShot(launchingShovel);*/
    }
}
