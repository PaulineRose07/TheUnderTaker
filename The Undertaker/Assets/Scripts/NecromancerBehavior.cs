using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NecromancerBehavior : MonoBehaviour {

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private PoolSystem m_poolSystem;
    [SerializeField] private GameObject m_player;
    [Header("--- Infos ---")]
    [SerializeField] private int m_damagesToPlayer;
    [SerializeField] private int m_lives;
    [SerializeField] private int m_pointsToScore;
    [SerializeField] private float m_speedOfMovement;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Collider2D m_collider2D;
    [Header("--- Audio ---")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private List<AudioClip> m_clipListExplosion;
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
    [SerializeField] private GameObject m_shieldPrefab;


    private void Start() {
        m_spriteRenderer.enabled = true;
        m_timerChangeFromTopToRight = m_timerDirectionTop;
    }

    private void Update() {
        MovementsTopOfScreen();
        //MovementRightSide();
        /*m_timerChangeFromTopToRight -= Time.deltaTime;
        if (m_timerChangeFromTopToRight < 0) 
        {
            transform.position = new Vector3(7,1, 0);
            MovementRightSide();
            m_timerChangeFromTopToRight;
        }
        */

        m_shieldTimer -= Time.deltaTime;
        if (m_shieldTimer < 0) {
            DeactivateShield();
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


    private void MovementsLeftSide() 
    {

    }

    private void MovementRightSide() {
        if (dirUp)
            transform.Translate(Vector2.up * m_speedOfMovement * Time.deltaTime);
        else
            transform.Translate(-Vector2.up * m_speedOfMovement * Time.deltaTime);

        if (transform.position.y >= 3.0f) {
            dirUp = false;
        }

        if (transform.position.y <= -3) {
            dirUp = true;
        }
    }
    private void MovementsTopOfScreen() {
        if (dirRight)
            transform.Translate(Vector2.right * m_speedOfMovement * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * m_speedOfMovement * Time.deltaTime);

        if (transform.position.x >= 4.0f) {
            dirRight = false;
        }

        if (transform.position.x <= -4) {
            dirRight = true;
        }

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

    }

    public void ShowYourself() {
        m_spriteRenderer.enabled = true;
        if (m_trailParticles != null) m_trailParticles.Play();
    }

    IEnumerator ParticlesShow()
    {
        
        yield return new WaitForSeconds(.5f);
    }

    public void HideYourself() {
        m_spriteRenderer.enabled = false;
        if (m_trailParticles != null) m_trailParticles.Stop();
    }


}
