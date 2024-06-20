using System.Collections;
using System.Collections.Generic;
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




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            m_gameManager.DecreaseLives(m_damagesToPlayer);
        }

    }

    public virtual void OnTriggerReaction()
    {

    }

    public virtual void OnTriggerExitReaction()
    {

    }

    abstract public void TouchedByHeroProjectile();

    public void ShowYourself()
    {
        m_spriteRenderer.enabled = true;
    }

    public void HideYourself()
    {
        m_spriteRenderer.enabled = false;
    }

    public void LootOnDeath()
    {
        int dropChance = Random.Range(0, 10);
        if (m_difficulty == 3 && dropChance >= 9)
            m_spawnManager.SpawnShieldPotion(transform.position);
        if (m_difficulty >= 2 && dropChance >= 8)
            m_spawnManager.SpawnSpeedPotion(transform.position);
        if (m_difficulty >= 1 && dropChance >= 7)
            m_spawnManager.SpawnHealthPotion(transform.position);
    }

    
}
