using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyBase
{

    private void Start() 
    {
        m_spriteRenderer.enabled = false;
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
        if (m_player.transform.position.x > transform.position.x) {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;
    }
}
