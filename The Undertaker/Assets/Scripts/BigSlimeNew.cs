using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeNew : EnemyBase
{
    [SerializeField] private int m_sizeOfSlime;

    private void Start()
    {
        m_spriteRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, m_speedOfMovement * Time.deltaTime);
        if (m_player.transform.position.x > transform.position.x)
        {
            m_spriteRenderer.flipX = true;
        }
        else m_spriteRenderer.flipX = false;
    }

    public override void TouchedByHeroProjectile()
    {
        if (m_lives <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        m_gameManager.UpdateScore(m_pointsToScore);
        m_gameManager.m_amountOfSpawns--;
        SplitWhenDead();
        SplitWhenDead();
        m_collider2D.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void SplitWhenDead()
    {
        if(m_sizeOfSlime == 3)
        {
            GameObject instanceSplit = m_poolSystem.GetMediumSlime();
            //instanceSplit.transform.position = transform.position;
            SlimeSplit(instanceSplit);

        }
        if(m_sizeOfSlime == 2)
        {
            GameObject instanceSplit = m_poolSystem.GetSmallSlime();
            SlimeSplit(instanceSplit);
        }
    }

    private void SlimeSplit(GameObject _instance)
    {
        _instance.transform.position = transform.position + (Vector3)Random.insideUnitCircle;
        _instance.SetActive(true);
        var SplitScript = _instance.GetComponent<EnemyBase>();
        SplitScript.m_gameManager = m_gameManager;
        SplitScript.m_poolSystem = m_poolSystem;
        SplitScript.m_collider2D.enabled = true;
        var SplitSlimeScript = _instance.GetComponent<BigSlimeNew>();
        SplitSlimeScript.m_player = m_gameManager.m_player;
        m_gameManager.m_amountOfSpawns++;

    }
}
