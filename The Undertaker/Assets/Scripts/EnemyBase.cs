using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public GameManager m_gameManager;
    public SpawnManager m_spawnManager;
    public PoolSystem m_poolSystem;
    public GameObject m_player;
    public int m_damagesToPlayer;
    public int m_lives;
    public int m_pointsToScore;
    public float m_speedOfMovement;
    public SpriteRenderer m_spriteRenderer;
    public Collider2D m_collider2D;
    [SerializeField] private int m_difficulty;
    public AudioSource m_audioSource;
    public List<AudioClip> m_clipListExplosion;
    public ParticleSystem m_explodingParticles;
    public ParticleSystem m_trailParticles;
    public int m_maxLives;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovements>(out PlayerMovements component))
        {
            m_gameManager.DecreaseLives(m_damagesToPlayer);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovements>(out PlayerMovements component))
            m_gameManager.DecreaseLives(m_damagesToPlayer);
    }

    public virtual void OnTriggerReaction()
    {

    }

    public virtual void OnTriggerExitReaction()
    {

    }

    public virtual void ResetOnSpawn()
    {
        m_lives = m_maxLives;
    }

    abstract public void TouchedByHeroProjectile();

    public void ShowYourself()
    {
        m_spriteRenderer.enabled = true;
        if (m_trailParticles != null) m_trailParticles.Play();
    }

    public void HideYourself()
    {
        m_spriteRenderer.enabled = false;
        if(m_trailParticles != null) m_trailParticles.Stop();
    }

    public void LootOnDeath()
    {
        int dropChance = Random.Range(0, 15);
        if (m_difficulty == 3 && dropChance == 2)
            m_spawnManager.SpawnShieldPotion(transform.position);
        if (m_difficulty >= 2 && dropChance == 1)
            m_spawnManager.SpawnSpeedPotion(transform.position);
        if (m_difficulty >= 1 && dropChance == 0)
            m_spawnManager.SpawnHealthPotion(transform.position);
    }

    
}
