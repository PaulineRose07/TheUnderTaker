using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_uiLives;
    [SerializeField] private TMP_Text m_uiScore;
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private Image m_gameOverImage;

    // Update is called once per frame
    void Update()
    {
        m_uiLives.text = "Lives : " + m_gameManager.m_currentLives.ToString();
        m_uiScore.text = m_gameManager.m_currentScore.ToString();
    }

    public void OpenSettingsMenu()
    {

    }

    public void CloseSettingsMenu()
    {

    }

    public void YouWonTheGame()
    {

    }

    public void GameOverOverlayOpen()
    {
        m_gameOverPanel.SetActive(true);
        var img = m_gameOverImage.GetComponent<Image>();
    }

    public void GameOverOverlayClose()
    {

    }
}
