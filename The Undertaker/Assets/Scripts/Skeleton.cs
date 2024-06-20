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
    private Vector3 m_direction;

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
        m_explodingParticles.Play();
        m_collider2D.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (m_player.transform.position.x > transform.position.x) {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;
        ShootWhenHidden();
        MoveWhenSeen();
    }

    public override void OnTriggerReaction()
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
            m_timerChangeDirection -= Time.deltaTime;
            if(m_timerChangeDirection <= 0)
            {
                float m_directionX = Random.Range(-1f, 1f);
                float m_directionY = Random.Range(-1f, 1f);
                m_direction = new Vector3(m_directionX, m_directionY,0).normalized;

                m_timerChangeDirection = m_directionDelay;
            }
            transform.Translate(m_speedOfMovement * Time.deltaTime * m_direction, Space.World);
        }
    }

    public override void OnTriggerExitReaction()
    {
        m_isShooting = true;
        m_fireTimer = m_fireDelay;
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
        var bone = m_poolSystem.GetAvailableBone();
        bone.transform.position = transform.position;

        //bone.transform.LookAt(m_player.transform.position);
        //bone.transform.rotation = transform.rotation;
        bone.SetActive(true);
        EnemyProjectileBase boneScript = bone.GetComponent<EnemyProjectileBase>();
        boneScript.ActivateSpriteRenderer();
        boneScript.m_gameManager = m_gameManager;
        var direction = m_player.transform.position - bone.transform.position;
        boneScript.m_direction = direction;
        //AudioClip launchingShovel = m_launchShovel[Random.Range(0, m_launchShovel.Count)];
        //m_audioSource.PlayOneShot(launchingShovel);
    }
}
