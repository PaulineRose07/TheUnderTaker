using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PoolSystem m_poolSystem;
    [SerializeField] GameManager m_gameManager;
    [SerializeField] public List<GameObject> m_spawnPoints = new List<GameObject>();
    [SerializeField] Sprite m_openedGrave;
    private float m_timerSlime;
    [SerializeField] float m_delayerSlimeSpawn = 2f;
    [SerializeField] int m_maximumAmountOfSpawns = 4;
    [SerializeField] public int m_addingSpawnCount;
    [SerializeField] public ChamberManager m_chamberManager;
    [SerializeField] private int m_difficultyLevel;


    // Start is called before the first frame update
    void Start()
    {
        m_poolSystem = m_chamberManager.m_poolSystem;
        m_timerSlime = m_chamberManager.m_timerForSpawn;
        m_maximumAmountOfSpawns = m_chamberManager.m_maxSpawnOfEnemies;
        m_difficultyLevel = m_chamberManager.m_difficultyLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_difficultyLevel == 0) return;
        if (m_difficultyLevel == 1)
        {
            if (m_addingSpawnCount < m_maximumAmountOfSpawns)
            {
                m_timerSlime -= Time.deltaTime;
                if (m_timerSlime <= 0 && m_spawnPoints.Count > 0)
                {
                    SpawnSlime(Random.Range(1, 4));
                    m_gameManager.m_amountOfSpawns++;
                }
            }
        }

        if(m_difficultyLevel == 2)
        {
            if (m_addingSpawnCount < m_maximumAmountOfSpawns) 
            { 
                m_timerSlime -= Time.deltaTime;
                if (m_timerSlime <= 0 && m_spawnPoints.Count > 0)
                {
                    int methodSelected = Random.Range(0, 2);
                    if(methodSelected == 0)
                    {
                        SpawnSlime(Random.Range(1, 4));
                        m_gameManager.m_amountOfSpawns++;
                    }
                    if(methodSelected == 1)
                    {
                        SpawnMiniNecromancer();
                        //m_gameManager.m_amountOfSpawns++;
                    }    
                    m_timerSlime = m_delayerSlimeSpawn;
                }
            }
        }
    }


    private void SpawnSlime(int _sizeOfSlime)
    {
        GameObject instanceSplit;
        if( _sizeOfSlime == 3)
        {
            instanceSplit = m_poolSystem.GetBigSlime();
        }
        else if( _sizeOfSlime == 2) instanceSplit = m_poolSystem.GetMediumSlime();
        else instanceSplit = m_poolSystem.GetSmallSlime();

        GameObject randomSpawnPoint = m_spawnPoints[Random.Range(0,m_spawnPoints.Count)];
        randomSpawnPoint.GetComponent<GraveBehavior>().ChangeGraveWhenOpen();
        m_spawnPoints.Remove(randomSpawnPoint);

        instanceSplit.transform.position = randomSpawnPoint.transform.position;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
    }

    private void SpawnMiniNecromancer()
    {
        Debug.Log("Mini Necromancer");
    }
    /*private void SpawnBigSlime() 
    {
        GameObject instanceSplit = m_poolSystem.GetBigSlime();
        instanceSplit.transform.position = Random.insideUnitCircle * m_spawnRadius;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
    }
    private void SpawnMediumSlime()
    {
        GameObject instanceSplit = m_poolSystem.GetMediumSlime();
        instanceSplit.transform.position = Random.insideUnitCircle * m_spawnRadius;
        instanceSplit.SetActive(true);
        
    }

    private void SpawnSmallSlime()
    {
        GameObject instanceSplit = m_poolSystem.GetSmallSlime();
        instanceSplit.transform.position = Random.insideUnitCircle * m_spawnRadius;
        instanceSplit.SetActive(true);
    }*/


}
