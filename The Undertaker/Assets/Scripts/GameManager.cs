using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    [Header("--- In Scene direct Links ---")]
    [SerializeField] public GameObject m_player;
    [SerializeField] private uiManager m_uiManager;
    [SerializeField] private HealthBar m_healthBar;
    [SerializeField] private NecromancerBehavior m_boss;
    [SerializeField] private AudioClip m_doorUnlockSound;
    [SerializeField] private AudioSource m_audioSourceDoor;
    [Space(16)]

    [Header("--- Chamber Links ---")]
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] private CameraShake m_camera;
    //[SerializeField] private DoorBehaviour m_doorBehaviour;
    [SerializeField] private Light m_directionalLight;
    [SerializeField] private SpawnManager m_spawnManager;
    [SerializeField] private List<DoorBehaviour> m_doors;
    [Space(8)]
    [SerializeField] public int m_amountOfSpawns;
    [SerializeField] public List<ChamberManager> m_chamberManagers;

    [Space(16)]
    [Header("--- Player Information --- ")]
    [SerializeField] public int m_currentScore;
    [SerializeField] public int m_currentLives;



    // Start is called before the first frame update
    void Start() {
        m_currentLives = m_player.GetComponent<PlayerInformation>().m_maxLives;
        m_healthBar.m_maxValue = m_currentLives;
        InitializeFirstChamber(m_chamberManagers[0]);
    }

    // Update is called once per frame
    void Update() {
        if (m_spawnManager.m_spawnPoints.Count == 0 && m_amountOfSpawns == 0) {
            Debug.Log("Wesh");
            m_spawnManager.RefreshSpawnPointsSprite();
            /*foreach (DoorBehaviour door in m_doors)
            {
                door.UnlockDoor();
            }
            //add here that the bool for the visited chamber is now true
            m_directionalLight.DOIntensity(.5f, .5f);*/

            StartCoroutine(ChamberCleaned());
        }

        if(m_currentLives <= 0)
        {
            m_uiManager.GameOverOverlayOpen();
        }

        if(m_boss.m_lives <= 0 && m_amountOfSpawns == 0)
        {
            YouWon();
        }
    }

    private void InitializeFirstChamber(ChamberManager _newChamberManager) {
        m_amountOfSpawns = 0;
        m_virtualCamera = _newChamberManager.m_virtualCamera;
        m_spawnManager = _newChamberManager.m_spawnManager;
        //m_doorBehaviour = _newChamberManager.m_doorBehaviour;
        m_doors = _newChamberManager.m_doors;
        foreach(DoorBehaviour door in m_doors)
        {
            door.LockDoors();
        }
        //m_directionalLight = _newChamberManager.m_directionalLight;
        m_virtualCamera.Priority = 1;
        m_spawnManager.m_canSpawn = true;
        _newChamberManager.GetComponentInChildren<RoomTrigger>().gameObject.SetActive(false);
    }
    public void InitializeChamber(ChamberManager _chamberManager)
    {
        m_amountOfSpawns = 0;
        m_spawnManager = _chamberManager.m_spawnManager;
        //m_doorBehaviour = _chamberManager.m_doorBehaviour;
        m_doors = _chamberManager.m_doors;
        foreach (DoorBehaviour door in m_doors)
        {
            door.LockDoors();
        }
        //m_directionalLight = _chamberManager.m_directionalLight;
        m_directionalLight.intensity = 0.2f;
        m_spawnManager.m_canSpawn = true;
    }

    public void SwitchCamera(ChamberManager _chamberManager)
    {
        var previousCam = m_virtualCamera;
        m_virtualCamera = _chamberManager.m_virtualCamera;
        previousCam.Priority = 0;
        m_virtualCamera.Priority = 1;
        m_camera = _chamberManager.m_camera;
    }
    public void ResetWhenEnteringNewRoom()
    {
        m_amountOfSpawns = 0;

    }

    public void UpdateScore(int _score)
    {
        m_currentScore += _score;
 
    }

    public void IncreaseLives(int _lives)
    {
        if (m_currentLives >= m_player.GetComponent<PlayerInformation>().m_maxLives) return;
        m_currentLives += _lives;
        m_healthBar.ChangeHealthBar(_lives);
    }

    public void DecreaseLives(int _lives)
    {
        if (m_player.GetComponent<PlayerMovements>().m_isShielded) return;
   
        m_currentLives -= _lives;
        m_healthBar.ChangeHealthBar(-_lives);
        m_camera.ShakeCamera(1, .2f);
        m_player.GetComponent<PlayerMovements>().LoseOneLife();
        
    }

    IEnumerator YouWon()
    {
        m_directionalLight.DOIntensity(.5f, .5f);
        yield return new WaitForSeconds(3);
        m_uiManager.YouWonTheGame();
    }

    
    IEnumerator ChamberCleaned()
    {
        m_directionalLight.DOIntensity(.5f, .5f);
        yield return new WaitForSeconds(1);
        //if(m_doorUnlockSound != null) m_audioSourceDoor.Play(m_doorUnlockSound);
        foreach (DoorBehaviour door in m_doors)
        {
            door.UnlockDoor();
        }
    }


}
