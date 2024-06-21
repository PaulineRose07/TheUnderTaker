using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNecromancer : EnemyBase
{
    [SerializeField] private float m_timerCount;
    [SerializeField] private float m_offset = .5f;
    [SerializeField] List<Transform> m_spawnPointsFront = new List<Transform>();
    [SerializeField] List<Transform> m_spawnPointsBack = new List<Transform>();
    private float m_timer;
    [SerializeField] private float m_directionDelay= 2f;
    [SerializeField] private Vector3[] m_teleportPoints;
    [SerializeField] private float m_fleeDelay;
    private float m_fleeTimer = .5f;
    private bool m_isHidden;

    private void Start() 
    {
        m_isHidden = true;
        m_spriteRenderer.enabled = false;
        FirstTeleport();
    }

    public override void TouchedByHeroProjectile() {
        if (m_lives <= 0) {
            StartCoroutine(EnemyDeath());
        }
        else StartCoroutine(BlinkWhenHurt());
    }

    IEnumerator BlinkWhenHurt()
    {
        var originColor = m_spriteRenderer.color;
        m_spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.color = originColor;
        m_spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.color = originColor;
    }
    

    private void Update() 
    {
        //transform.position = Vector2.MoveTowards(m_player.transform.position, transform.position, m_speedOfMovement * Time.deltaTime);
        // Stick to a Corner of the room
        /*m_timerChangeDirection -= Time.deltaTime;
        if (m_timerChangeDirection <= 0)
        {
            float m_directionX = Random.Range(-1f, 1f);
            float m_directionY = Random.Range(-1f, 1f);
            m_direction = new Vector3(m_directionX, m_directionY, 0).normalized;

            m_timerChangeDirection = m_directionDelay;
        }
        transform.Translate(m_speedOfMovement * Time.deltaTime * m_direction, Space.World);*/


        if (m_player.transform.position.x > transform.position.x) {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;

        m_timer -= Time.deltaTime;
         if(m_timer <= 0 ) {
             SpawnSlimes(Random.Range(1, 3));
             m_timer = m_timerCount;
         }

        TeleportWhenSeen();

    }

    public override void OnTriggerReaction()
    {
        m_isHidden = false;
        m_fleeTimer = m_fleeDelay;
    }

    private void TeleportWhenSeen()
    {
        if (m_isHidden) return;
        m_fleeTimer -= Time.deltaTime;
        if(m_fleeTimer <= 0)
        {
            Vector3 randomCorner = m_teleportPoints[Random.Range(0,m_teleportPoints.Length)];
            transform.position = randomCorner;
        }
    }

    private void FirstTeleport()
    {
        Vector3 randomCorner = m_teleportPoints[Random.Range(0, m_teleportPoints.Length)];
        transform.position = randomCorner;
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

        Transform randomSpawnPoint;
        if (m_spriteRenderer.flipX)
        {
            randomSpawnPoint = m_spawnPointsBack[Random.Range(0, m_spawnPointsBack.Count)];
        }
        else randomSpawnPoint = m_spawnPointsFront[Random.Range(0, m_spawnPointsFront.Count)];

        instanceSplit.transform.position = randomSpawnPoint.position;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
    }

}
