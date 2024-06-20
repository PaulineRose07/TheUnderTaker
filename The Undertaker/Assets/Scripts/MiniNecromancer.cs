using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNecromancer : EnemyBase
{
    [SerializeField] private float m_timerCount;
    [SerializeField] private float m_offset = .5f;
    private float m_timer;
    private float m_timerChangeDirection;
    private Vector3 m_direction;
    [SerializeField] private float m_directionDelay= 2f;

    private void Start() 
    {
        m_timerChangeDirection = 0;
        m_spriteRenderer.enabled = false;
    }

    public override void TouchedByHeroProjectile() {
        if (m_lives <= 0) {
            StartCoroutine(EnemyDeath());
        }
    }

    private void Update() 
    {
        //transform.position = Vector2.MoveTowards(m_player.transform.position, transform.position, m_speedOfMovement * Time.deltaTime);
        // Stick to a Corner of the room
        m_timerChangeDirection -= Time.deltaTime;
        if (m_timerChangeDirection <= 0)
        {
            float m_directionX = Random.Range(-1f, 1f);
            float m_directionY = Random.Range(-1f, 1f);
            m_direction = new Vector3(m_directionX, m_directionY, 0).normalized;

            m_timerChangeDirection = m_directionDelay;
        }
        transform.Translate(m_direction * m_speedOfMovement, Space.World);

        m_timer -= Time.deltaTime;
         if(m_timer <= 0 ) {
             SpawnSlimes(Random.Range(1, 3));
             m_timer = m_timerCount;
         }

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

    private void SpawnSlimes(int _sizeOfSlime) {
        GameObject instanceSplit;
        if (_sizeOfSlime == 3) {
            instanceSplit = m_poolSystem.GetBigSlime();
        }
        else if (_sizeOfSlime == 2) instanceSplit = m_poolSystem.GetMediumSlime();
        else instanceSplit = m_poolSystem.GetSmallSlime();

    
        instanceSplit.transform.position = transform.forward * m_offset ;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
       
    }

}
