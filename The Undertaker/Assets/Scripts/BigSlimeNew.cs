using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeNew : EnemyBase
{
    [SerializeField] private int m_sizeOfSlime;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, m_speedOfMovement * Time.deltaTime);
    }

    public override void TouchedByHeroProjectile()
    {
        if (m_lives == 0)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        m_gameManager.UpdateScore(m_pointsToScore);
        SplitWhenDead();
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void SplitWhenDead()
    {
        if(m_sizeOfSlime == 3)
        {
            GameObject instanceSplit = m_poolSystem.GetMediumSlime();
            instanceSplit.transform.position = transform.position;
            instanceSplit.SetActive(true);
            var SplitScript = instanceSplit.GetComponent<EnemyBase>();
            SplitScript.m_gameManager = m_gameManager;
            SplitScript.m_poolSystem = m_poolSystem;
            var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
            SplitSlimeScript.m_player = m_gameManager.m_player;

        }
        if(m_sizeOfSlime == 2)
        {
            GameObject instanceSplit = m_poolSystem.GetSmallSlime();
            instanceSplit.transform.position = transform.position;
            instanceSplit.SetActive(true);
            var SplitScript = instanceSplit.GetComponent<EnemyBase>();
            SplitScript.m_gameManager = m_gameManager;
            SplitScript.m_poolSystem = m_poolSystem;
            var SplitSlimeScript = instanceSplit.GetComponent<BigSlimeNew>();
            SplitSlimeScript.m_player = m_gameManager.m_player;
        }
    }
}
