using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public int m_maxValue;
    [SerializeField] public int m_value;
    [SerializeField] private RectTransform m_topBarGreenTransform;
    [SerializeField] private RectTransform m_bottomBarRedTransform;

    private float m_healthBarMaxWidth;

    private float m_targetWidth => m_value * m_healthBarMaxWidth / m_maxValue;

    [SerializeField] private float m_animationSpeed = 10f;
    private Coroutine m_adjustBarWithCoroutine;


    void Start()
    {
        m_healthBarMaxWidth = m_topBarGreenTransform.rect.width;
    }

    public void ChangeHealthBar(int _amount)
    {
        m_value = Mathf.Clamp(m_value + _amount, 0, m_maxValue);

        if(m_adjustBarWithCoroutine != null)
        {
            StopCoroutine(m_adjustBarWithCoroutine);
        }
        m_adjustBarWithCoroutine = StartCoroutine(AdjustBarWidth(_amount));
    }

    private IEnumerator AdjustBarWidth(int _amount)
    {
        var suddenChangeBar = _amount >= 0 ? m_bottomBarRedTransform : m_topBarGreenTransform;

        var slowChangeBar = _amount >= 0 ? m_topBarGreenTransform : m_bottomBarRedTransform;

        suddenChangeBar.SetWidth(m_targetWidth);
        while(Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, m_targetWidth, Time.deltaTime * m_animationSpeed));
            yield return null;
        }

        slowChangeBar.SetWidth(m_targetWidth);
    }
    
}
