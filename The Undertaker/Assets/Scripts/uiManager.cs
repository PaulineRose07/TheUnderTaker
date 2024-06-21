using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_uiLives;
    [SerializeField] private TMP_Text m_uiScore;
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private GameObject m_bossPanel;
    [SerializeField] private GameObject m_settingsPanel;
    [SerializeField] private AudioSource m_bossMusicSource;
    [SerializeField] private AudioClip m_bossMusic;


    // Update is called once per frame
    void Update()
    {
        m_uiLives.text = "Lives : " + m_gameManager.m_currentLives.ToString();
        m_uiScore.text = m_gameManager.m_currentScore.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            //OpenSettingsMenu();
        }
    }
    public void BossPanelActivation()
    {
        m_bossPanel.SetActive(true);
        m_bossMusicSource.PlayOneShot(m_bossMusic);
    }
    public void OpenSettingsMenu()
    {
        Time.timeScale = 0f;
        m_settingsPanel.SetActive(true);
    }
  
    public void CloseSettingsMenu()
    {
        m_settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void YouWonTheGame()
    {
        SceneManager.LoadScene(2);
    }

    public void GameOverOverlayOpen()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitThisApp()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(3);
    }


}
