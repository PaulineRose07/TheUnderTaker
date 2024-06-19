using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject m_player;
    [SerializeField] private uiManager m_uiManager;
    [SerializeField] private CameraShake m_camera;
    [SerializeField] private SpawnManager m_spawnManager;
    [SerializeField] private DoorBehaviour m_doorBehaviour;
    [SerializeField] private Light m_directionalLight;
    [SerializeField] public int m_amountOfSpawns;

    [SerializeField] public int m_currentScore;
    [SerializeField] public int m_currentLives;



    // Start is called before the first frame update
    void Start()
    {
        m_currentLives = m_player.GetComponent<PlayerInformation>().m_maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_spawnManager.m_spawnPoints.Count == 0 && m_amountOfSpawns == 0)
        {

            m_doorBehaviour.UnlockDoor();
            m_directionalLight.intensity = 1.2f;
        }
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
        m_currentLives += _lives;
    }

    public void DecreaseLives(int _lives)
    {
        m_currentLives -= _lives;
        m_camera.ShakeCamera(2,.2f);
        m_player.GetComponent<PlayerMovements>().LoseOneLife();
    }
}
