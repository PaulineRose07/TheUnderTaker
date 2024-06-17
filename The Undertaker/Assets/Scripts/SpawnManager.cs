using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PoolSystem m_poolSystem;
    [SerializeField] GameManager m_gameManager;
    [SerializeField] float m_spawnRadius = 10;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnMediumSlime", 1,2);
        InvokeRepeating("SpawnBigSlime", 1, 3);
        //InvokeRepeating("SpawnSmallSlime", 4, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
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

        instanceSplit.transform.position = Random.insideUnitCircle * m_spawnRadius;
        instanceSplit.SetActive(true);
        var SplitScript = instanceSplit.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
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
