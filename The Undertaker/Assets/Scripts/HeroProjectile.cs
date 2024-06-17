using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProjectile : MonoBehaviour
{
    [SerializeField] public int m_damagesToEnemy;
    [SerializeField] private SpriteRenderer m_spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BulletTouchedSomething()
    {
        StartCoroutine(BulletLifeHasEnded());
    }

    IEnumerator BulletLifeHasEnded()
    {
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
    }
}
