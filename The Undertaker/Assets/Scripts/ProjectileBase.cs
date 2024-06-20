using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public SpriteRenderer m_spriteRenderer;
    public float m_speedOfProjectile = 10f;
    public int m_damagesToEnemy;
    public Collider2D m_collider;
    public AudioSource m_audioSource;
    public List<AudioClip> m_explosionClip;
    public ParticleSystem m_exlosionParticles;


    // Update is called once per frame

    public void ProjectileMovement()
    {
        transform.Translate(Vector2.up * Time.deltaTime * m_speedOfProjectile);
    }

    public void BulletTouchedSomething()
    {
        StartCoroutine(BulletLifeHasEnded());
    }

    IEnumerator BulletLifeHasEnded()
    {
        m_collider.enabled = false;
        m_spriteRenderer.enabled = false;
        m_exlosionParticles.Play();
        /*
        AudioClip explodingSound = m_explosionClip[Random.Range(0, m_explosionClip.Count)];
       
        m_audioSource.PlayOneShot(explodingSound);
        */
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
    }

    public void ActivateSpriteRenderer()
    {
        m_spriteRenderer.enabled = true;
        m_collider.enabled = true;
    }
}
