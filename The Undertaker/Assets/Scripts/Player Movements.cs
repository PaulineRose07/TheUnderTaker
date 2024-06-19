using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DG.Tweening;


public class PlayerMovements : MonoBehaviour
{
    Vector3 m_mousePosition;
    private float m_currentSpeed;
    [SerializeField] private float m_rotationSpeed = .8f;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    private float m_speedTimer;
    private float m_shieldTimer;
    [SerializeField] private GameObject m_shieldPrefab;
    [SerializeField] private float m_baseSpeed=10f;
    [SerializeField] public bool m_isShielded;

    // Start is called before the first frame update
    void Awake()
    {
        m_currentSpeed = m_baseSpeed;
        m_isShielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        float translationX = Input.GetAxis("Horizontal") * m_currentSpeed * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical") * m_currentSpeed * Time.deltaTime;
        transform.Translate(new Vector3(translationX, translationY, 0),Space.World);

        m_mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var targetRotation = Quaternion.LookRotation(Vector3.forward, m_mousePosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_rotationSpeed);

        m_speedTimer-=Time.deltaTime;
        if(m_speedTimer < 0)
        {
            m_currentSpeed = m_baseSpeed;
        }
        
        m_shieldTimer -=Time.deltaTime;
        if(m_shieldTimer < 0)
        {
            DeactivateShield();
        }
        /*
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
    }

    public void LoseOneLife()
    {
        StartCoroutine(BlinkWhenLosingLife());
    }

    IEnumerator BlinkWhenLosingLife()
    {
        yield return new WaitForSeconds(.2f);
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.enabled = true;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.1f);
        m_spriteRenderer.enabled = true;
    }

    public void AddSpeed(int _speed, float _speedTimer)
    {
        m_currentSpeed = _speed;
        m_speedTimer += _speedTimer;
    }

    public void AddShield(int _shieldTimer)
    {
        m_isShielded = true;
        m_shieldTimer += _shieldTimer;
        m_shieldPrefab.SetActive(true);
        m_shieldPrefab.transform.DOScale(2, 1.5f);
    }

    private void DeactivateShield()
    {   
        m_isShielded = false;
        m_shieldPrefab.SetActive(false);
        m_shieldPrefab.transform.localScale = Vector3.zero;
    }
    

}
