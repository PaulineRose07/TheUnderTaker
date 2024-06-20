using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class UIWinner : MonoBehaviour
{
    [SerializeField] RectTransform m_textRectTransform;
    private float m_PosY = 0f;
    private float m_PosYForNow = -113.3f;
    [SerializeField] private float m_timeToDescend = .3f;
    [SerializeField] GameObject m_buttonRetry;
    [SerializeField] GameObject m_buttonMainMenu;
    [SerializeField] GameObject m_buttonQuit;
    
    [Header("--- For now... ----")]
    [SerializeField] RectTransform m_necromamancer;
    [SerializeField] RectTransform m_textForNow;


    [Header("--- Fireworks ----")]
    [SerializeField] ParticleSystem m_firework01;
    [SerializeField] ParticleSystem m_firework02;
    [SerializeField] ParticleSystem m_firework03;
    [SerializeField] ParticleSystem m_fireworks04;
    [SerializeField] ParticleSystem m_fireworks05;
    [SerializeField] ParticleSystem m_fireworks06;

    // Update is called once per frame
    void Update() {
        StartCoroutine(ShowOnScreenCoroutine());
        StartCoroutine(Fireworks());
        StartCoroutine(ForNow());
    }

    IEnumerator ShowOnScreenCoroutine() {
        m_textRectTransform.DOAnchorPosY(m_PosY, m_timeToDescend);
        yield return new WaitForSeconds(1f);
        m_firework01.Play();
        m_firework02.Play();
        yield return new WaitForSeconds(.5f);
        m_buttonMainMenu.SetActive(true);
        m_buttonRetry.SetActive(true);
        yield return new WaitForSeconds(.5f);
        m_buttonQuit.SetActive(true);
    }

    IEnumerator Fireworks() 
    {
        yield return new WaitForSeconds(1f);
        m_firework03.Play();
        yield return new WaitForSeconds(.15f);
        m_fireworks04.Play();
        yield return new WaitForSeconds(.12f);
        m_fireworks05.Play();
        yield return new WaitForSeconds(.13f);
        m_fireworks06.Play();
    }

    IEnumerator ForNow() {
        yield return new WaitForSeconds(3f);
        m_textForNow.DOAnchorPosY(m_PosYForNow, m_timeToDescend);
        yield return new WaitForSeconds(.5f);
        m_necromamancer.DOAnchorPosY(m_PosY, m_timeToDescend);
    }
    public void QuitThisApp() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(3);
    }
}
