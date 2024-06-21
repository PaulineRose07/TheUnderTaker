using System.Collections.Generic;
using UnityEngine;

public class NecromancienSpawner : MonoBehaviour
{

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private PoolSystem m_poolSystem;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] List<Transform> m_spawnPointsFront = new List<Transform>();
    [SerializeField] List<Transform> m_spawnPointsBack = new List<Transform>();
    [SerializeField] private int m_difficultyLevel;
    [SerializeField] public bool m_canSpawn;
    [SerializeField] public float m_spawnDelay;
    private float m_spawnTimer;

    private void Awake()
    {
        m_canSpawn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_canSpawn = true;
        m_spawnTimer = m_spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_difficultyLevel == 1)
        {
            SpawnRandomSlime();
        }
        if (m_difficultyLevel == 2)
        {
            SpawnRandom();
        }
    }

    public void SpawnRandomSlime()
    {
        if (m_canSpawn)
        {
            m_spawnTimer -= Time.deltaTime;
                if (m_spawnTimer <= 0)
                {
                    SpawnSlime(Random.Range(1, 4));
                    m_gameManager.m_amountOfSpawns++;
                    m_spawnTimer = m_spawnDelay;
                }
        }
    }


    public void SpawnRandom()
    {
        if (m_canSpawn)
        {
            m_spawnTimer -= Time.deltaTime;
            if (m_spawnTimer <= 0)
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
                m_spawnTimer = m_spawnDelay;
            }
        }
    }
    private void SpawnSlime(int _sizeOfSlime)
    {
        GameObject instanceSplit;
        if (_sizeOfSlime == 3)
        {
            instanceSplit = m_poolSystem.GetBigSlime();
        }
        else if (_sizeOfSlime == 2) instanceSplit = m_poolSystem.GetMediumSlime();
        else instanceSplit = m_poolSystem.GetSmallSlime();

        Transform randomSpawnPoint;
        if(m_spriteRenderer.flipX)
        {
            randomSpawnPoint = m_spawnPointsBack[Random.Range(0, m_spawnPointsBack.Count)];
        }
        else randomSpawnPoint = m_spawnPointsFront[Random.Range(0, m_spawnPointsFront.Count)];


        instanceSplit.transform.position = randomSpawnPoint.position;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        //SplitScript.m_spawnManager = this;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
    }

    private void SpawnSkeleton()
    {
        GameObject instanceOfSkeleton = m_poolSystem.GetAvailableSkeleton();

        Transform randomSpawnPoint;
        if (m_spriteRenderer.flipX)
        {
            randomSpawnPoint = m_spawnPointsBack[Random.Range(0, m_spawnPointsBack.Count)];
        }
        else randomSpawnPoint = m_spawnPointsFront[Random.Range(0, m_spawnPointsFront.Count)];


        instanceOfSkeleton.transform.position = randomSpawnPoint.position;
        instanceOfSkeleton.SetActive(true);
        var SkeletonBase = instanceOfSkeleton.GetComponent<EnemyBase>();
        SkeletonBase.m_gameManager = m_gameManager;
        SkeletonBase.m_poolSystem = m_poolSystem;
        SkeletonBase.m_collider2D.enabled = true;
        var SkeletonScript = instanceOfSkeleton.GetComponent<Skeleton>();
        SkeletonScript.m_player = m_gameManager.m_player;

    }

    private void SpawnMiniNecromancer()
    {
        GameObject instanceOfMiniN = m_poolSystem.GetAvailableNecromancer();

        Transform randomSpawnPoint;
        if (m_spriteRenderer.flipX)
        {
            randomSpawnPoint = m_spawnPointsBack[Random.Range(0, m_spawnPointsBack.Count)];
        }
        else randomSpawnPoint = m_spawnPointsFront[Random.Range(0, m_spawnPointsFront.Count)];


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
}
