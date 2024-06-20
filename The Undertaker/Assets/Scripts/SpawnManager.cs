using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PoolSystem m_poolSystem;
    [SerializeField] GameManager m_gameManager;
    [SerializeField] public List<GameObject> m_spawnPoints = new List<GameObject>();
    private List<GameObject> m_usedSpawnPoints = new List<GameObject>();
    [SerializeField] Sprite m_openedGrave;
    private float m_timerSlime;
    [SerializeField] float m_delayerSlimeSpawn = 2f;
    [SerializeField] int m_maximumAmountOfSpawns = 4;
    [SerializeField] public int m_addingSpawnCount;
    [SerializeField] public ChamberManager m_chamberManager;
    [SerializeField] private int m_difficultyLevel;
    [SerializeField] public bool m_canSpawn;
    [SerializeField] private float m_skeletonOffset = 0.5f;

    private void Awake()
    {
        m_canSpawn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_poolSystem = m_chamberManager.m_poolSystem;
        m_gameManager = m_chamberManager.m_gameManager;
        m_timerSlime = m_chamberManager.m_timerForSpawn;
        m_maximumAmountOfSpawns = m_chamberManager.m_maxSpawnOfEnemies;
        m_difficultyLevel = m_chamberManager.m_difficultyLevel;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canSpawn) return;
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
                    m_timerSlime = m_delayerSlimeSpawn;
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
                        SpawnSkeleton();
                        m_gameManager.m_amountOfSpawns++;
                    }    
                    m_timerSlime = m_delayerSlimeSpawn;
                }
            }
        }

        if (m_difficultyLevel == 3)
        {
            if (m_addingSpawnCount < m_maximumAmountOfSpawns)
            {
                m_timerSlime -= Time.deltaTime;
                if (m_timerSlime <= 0 && m_spawnPoints.Count > 0)
                {
                    int methodSelected = Random.Range(0, 3);
                    if (methodSelected == 0)
                    {
                        SpawnSlime(Random.Range(1, 4));
                        m_gameManager.m_amountOfSpawns++;
                    }
                    if (methodSelected == 1)
                    {
                        SpawnSkeleton();
                        m_gameManager.m_amountOfSpawns++;
                    }
                    if (methodSelected == 2)
                    {
                        SpawnMiniNecromancer();
                        m_gameManager.m_amountOfSpawns++;
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
        m_usedSpawnPoints.Add(randomSpawnPoint);

        instanceSplit.transform.position = randomSpawnPoint.transform.position;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        SplitScript.m_spawnManager = this;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
    }

    public void SpawnHealthPotion(Vector3 _position)
    {
        GameObject instanceSplit = m_poolSystem.GetHealingPotion();
        PotionsDropping(instanceSplit, _position);
    }

    public void SpawnSpeedPotion(Vector3 _position)
    {
        GameObject instanceSplit = m_poolSystem.GetSpeedPotion();
        PotionsDropping(instanceSplit, _position);
    }

    public void SpawnShieldPotion(Vector3 _position)
    {
        GameObject instanceSplit = m_poolSystem.GetShieldPotion();
        PotionsDropping(instanceSplit, _position);
      
    }

    public void PotionsDropping(GameObject _potion, Vector3 _position)
    {
        _potion.transform.position = _position;
        _potion.SetActive(true);
        var SplitScript = _potion.GetComponent<PotionBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_player = m_gameManager.m_player;
    }

    private void SpawnMiniNecromancer()
    {
        GameObject instanceOfMiniN = m_poolSystem.GetAvailableNecromancer();

        GameObject randomSpawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
        randomSpawnPoint.GetComponent<GraveBehavior>().ChangeGraveWhenOpen();
        m_spawnPoints.Remove(randomSpawnPoint);
        m_usedSpawnPoints.Add(randomSpawnPoint);

        instanceOfMiniN.transform.position = randomSpawnPoint.transform.position;
        instanceOfMiniN.SetActive(true);
        var miniNecromancerBase = instanceOfMiniN.GetComponent<EnemyBase>();
        miniNecromancerBase.m_gameManager = m_gameManager;
        miniNecromancerBase.m_poolSystem = m_poolSystem;
        miniNecromancerBase.m_collider2D.enabled = true;
        var miniNecromancerScript = instanceOfMiniN.GetComponent<MiniNecromancer>();
        miniNecromancerScript.m_player = m_gameManager.m_player;

        Debug.Log("Mini Necromancer");
    }

    private void SpawnSkeleton()
    {
        GameObject instanceOfSkeleton = m_poolSystem.GetAvailableSkeleton();

        GameObject randomSpawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
        randomSpawnPoint.GetComponent<GraveBehavior>().ChangeGraveWhenOpen();
        m_spawnPoints.Remove(randomSpawnPoint);
        m_usedSpawnPoints.Add(randomSpawnPoint);

        instanceOfSkeleton.transform.position = randomSpawnPoint.transform.position * m_skeletonOffset;
        instanceOfSkeleton.SetActive(true);
        var SkeletonBase = instanceOfSkeleton.GetComponent<EnemyBase>();
        SkeletonBase.m_gameManager = m_gameManager;
        SkeletonBase.m_poolSystem = m_poolSystem;
        SkeletonBase.m_collider2D.enabled = true;
        var SkeletonScript = instanceOfSkeleton.GetComponent<Skeleton>();
        SkeletonScript.m_player = m_gameManager.m_player;

    }


    public void RefreshSpawnPointsSprite()
    {
        for (int i = 0; i < m_usedSpawnPoints.Count; i++)
        {
            m_usedSpawnPoints[i].GetComponent<GraveBehavior>().MaterialChangeToUnlit();
        }
    }
}
