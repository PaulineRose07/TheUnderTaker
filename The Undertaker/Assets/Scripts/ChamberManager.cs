using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class ChamberManager : MonoBehaviour
{
    [Header("--- Outside elements ---")]
    [SerializeField] public GameManager m_gameManager;
    [SerializeField] public uiManager m_uiManager;
    [SerializeField] public PoolSystem m_poolSystem;
    public GameObject m_player;
    [Space(16)]
    [Header("--- Inside Chamber links ---")]
    [SerializeField] public SpawnManager m_spawnManager;
    [SerializeField] public CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] public CameraShake m_camera;
    //[SerializeField] public DoorBehaviour m_doorBehaviour;
    //[SerializeField] public Light m_directionalLight;
    [SerializeField] public List<DoorBehaviour> m_doors;
    [Space(16)]
    [Header("--- Level Information ---")]
    [SerializeField] public int m_maxSpawnOfEnemies;
    [SerializeField] public int m_difficultyLevel;
    [SerializeField] public int m_amountOfActiveDoors;
    [SerializeField] public float m_timerForSpawn;

    [SerializeField] public bool m_hasAlreadyBeenVisited;

    private void Awake() {
        m_gameManager = FindObjectOfType<GameManager>();
        m_player = m_gameManager.m_player;
        m_uiManager = FindObjectOfType<uiManager>();
        m_poolSystem = FindObjectOfType<PoolSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_hasAlreadyBeenVisited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
