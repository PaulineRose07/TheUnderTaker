using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNecromancer : EnemyBase
{
    [SerializeField] private float m_timerCount;
    private float m_timer;

    private void Start() 
    {
        m_spriteRenderer.enabled = false;
    }

    public override void TouchedByHeroProjectile() {
        if (m_lives <= 0) {
            StartCoroutine(EnemyDeath());
        }
    }

    private void Update() {
        // Stick to a Corner of the room

        /*m_timer -= Time.deltaTime;
         if(m_timer <= 0 ) {
             SpawnSlimes(Random.Range(1, 4));
             m_timer = m_timerCount;
         }*/

        if (m_player.transform.position.x > transform.position.x) {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;
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

    /*private void SpawnSlimes(int _sizeOfSlime) {
        GameObject instanceSplit;
        if (_sizeOfSlime == 3) {
            instanceSplit = m_poolSystem.GetBigSlime();
        }
        else if (_sizeOfSlime == 2) instanceSplit = m_poolSystem.GetMediumSlime();
        else instanceSplit = m_poolSystem.GetSmallSlime();
        ////
    }*/

}
