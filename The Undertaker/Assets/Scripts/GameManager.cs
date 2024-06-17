using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private uiManager m_uiManager;

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
        
    }

    public void UpdateScore(int _score)
    {
        m_currentScore += _score;
    }

    public void UpdateLives(int _lives)
    {
        m_currentLives -= _lives;
    }
}
