using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] RectTransform m_textRectTransform;
    private float m_PosY = 0f;
    [SerializeField] private float m_timeToDescend = .3f;
    [SerializeField] GameObject m_buttonRetry;
    [SerializeField] GameObject m_buttonMainMenu;
    [SerializeField] GameObject m_buttonQuit;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShowOnScreenCoroutine());
        //m_textRectTransform.DOAnchorPosY(m_PosY, m_timeToDescend);
       
    }

    IEnumerator ShowOnScreenCoroutine()
    {
        m_textRectTransform.DOAnchorPosY(m_PosY, m_timeToDescend);
        yield return new WaitForSeconds(2f);
        m_buttonMainMenu.SetActive(true);
        m_buttonRetry.SetActive(true);
        yield return new WaitForSeconds(.5f);
        m_buttonQuit.SetActive(true);
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
