using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PoolSystem m_poolSystem;
    [SerializeField] GameManager m_gameManager;
    [SerializeField] List<GameObject> m_spawnPoints = new List<GameObject>();
    [SerializeField] Sprite m_openedGrave;
    private float m_timerSlime;
    [SerializeField] float m_delayerSlimeSpawn = 2f;
    


    // Start is called before the first frame update
    void Start()
    {
        m_timerSlime = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        m_timerSlime -= Time.deltaTime;
        if (m_timerSlime <= 0 && m_spawnPoints.Count > 0)
        {
            int methodSelected = Random.Range(0, 2);
            if(methodSelected == 0)
            {
                SpawnSlime(Random.Range(1, 4));
            }
            if(methodSelected == 1)
            {
                SpawnMiniNecromancer();
            }

           
            m_timerSlime = m_delayerSlimeSpawn;
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
        randomSpawnPoint.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        randomSpawnPoint.GetComponent<Collider2D>().enabled = false;
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
