using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NecromancerBehavior : MonoBehaviour {

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private uiManager m_uiManager;
    [SerializeField] private PoolSystem m_poolSystem;
    [SerializeField] private GameObject m_player;
    [SerializeField] private NecromancienSpawner m_spawner;
    [Header("--- Infos ---")]
    [SerializeField] private int m_damagesToPlayer;
    [SerializeField] public int m_lives;
    [SerializeField] private HealthBar m_healthBar;
    [SerializeField] private int m_pointsToScore;
    [SerializeField] private float m_speedOfMovement;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Collider2D m_collider2D;
    [Header("--- Audio ---")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private List<AudioClip> m_clipListExplosion;
    [SerializeField] private AudioClip m_laugh;
    [SerializeField] private AudioClip m_death;
    [SerializeField] private AudioClip m_spawn;
    [Header("--- Particles ---")]
    [SerializeField] private ParticleSystem m_explodingParticles;
    [SerializeField] private ParticleSystem m_trailParticles;
    [SerializeField] private ParticleSystem m_showOffParticles;
    [Header("--- Bools ---")]
    [SerializeField] private bool dirRight;
    [SerializeField] private bool dirUp;
    [SerializeField] private float m_timerDirectionTop = 1f;
    private float m_timerChangeFromTopToRight;
    private bool m_isShielded;
    private float m_shieldTimer;
    [SerializeField] private int m_ShieldLife = 5;
    [SerializeField] private GameObject m_shieldPrefab;
    [SerializeField] Vector3 m_topScreen = new Vector3(0, 2, 0);
    [SerializeField] Vector3 m_rightScreen = new Vector3(6, 0, 0);
    [SerializeField] Vector3 m_LeftScreen = new Vector3(6, 0, 0);
    Vector3 teleportPosition;
    private int m_randomVector3;

    private void Start() {
        m_isShielded = false;
        m_spriteRenderer.enabled = true;
        m_timerChangeFromTopToRight = m_timerDirectionTop;
        m_healthBar.m_maxValue = m_lives;
        m_gameManager.m_amountOfSpawns++;
        m_collider2D.enabled = false;

        
        StartCoroutine(FirstTeleport());
    }

    private void Update() {

        m_shieldTimer -= Time.deltaTime;
        if (m_shieldTimer < 0) {
            DeactivateShield();
        }

        if(m_lives == 10)
        {
            AddShield(m_ShieldLife);
        }

        if(m_lives == 5)
        {
            AddShield(m_ShieldLife);
        }


    }

    public void AddShield(int _shieldTimer) {
        m_isShielded = true;
        m_shieldTimer += _shieldTimer;
        m_shieldPrefab.SetActive(true);
        m_shieldPrefab.transform.DOScale(2, 1.5f);
    }

    private void DeactivateShield() {
        m_isShielded = false;
        m_shieldPrefab.SetActive(false);
        m_shieldPrefab.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<PlayerMovements>(out PlayerMovements component)) {
            m_gameManager.DecreaseLives(m_damagesToPlayer);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent<PlayerMovements>(out PlayerMovements component))
            m_gameManager.DecreaseLives(m_damagesToPlayer);
    }

    public void TouchedByHeroProjectile() 
    {
        if (m_lives <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
        else
        {
            
            StartCoroutine(BlinkWhenHurt());

        }
    }

    IEnumerator EnemyDeath()
    {
        m_gameManager.UpdateScore(m_pointsToScore);
        m_gameManager.m_amountOfSpawns--;
        m_audioSource.PlayOneShot(m_death);
        m_explodingParticles.Play();
        m_collider2D.enabled = false;
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    IEnumerator BlinkWhenHurt()
    {
        var originColor = m_spriteRenderer.color;
        m_spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.color = originColor;
        m_spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.color = originColor;
    }

    public void LoseLife(int _damages)
    {
        m_lives -= _damages;
        m_healthBar.ChangeHealthBar(-m_lives);
    }

    public void ShowYourself() {
        m_spriteRenderer.enabled = true;
        if (m_trailParticles != null) m_trailParticles.Play();
    }

    private IEnumerator FirstTeleport()
    {
        m_showOffParticles.Play();
        m_audioSource.PlayOneShot(m_spawn);
        yield return new WaitForSeconds(.5f);
        m_spriteRenderer.enabled = true;
        m_audioSource.PlayOneShot(m_laugh);
        yield return new WaitForSeconds(.1f);
        m_collider2D.enabled = true;
        m_spriteRenderer.enabled = false;
        transform.localScale = Vector3.one;
        m_uiManager.BossPanelActivation();
    }

    private IEnumerator Teleport()
    {
        transform.DOScale(new Vector3(0, 1, 1), .2f);
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.enabled = false;
        transform.localScale = Vector3.one;

        m_randomVector3 = Random.Range(0, 2);
        if (m_randomVector3 == 0) teleportPosition = m_topScreen;
        if (m_randomVector3 == 1) teleportPosition = m_rightScreen;
        if (m_randomVector3 == 2) teleportPosition = m_LeftScreen;

        transform.position = teleportPosition;
    }

    public void HideYourself() {
        m_spriteRenderer.enabled = false;
        if (m_trailParticles != null) m_trailParticles.Stop();
    }


}
