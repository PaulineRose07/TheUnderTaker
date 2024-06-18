using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    Vector3 m_mousePosition;
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_rotationSpeed = .8f;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        float translationX = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
        transform.Translate(new Vector3(translationX, translationY, 0),Space.World);

        m_mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var targetRotation = Quaternion.LookRotation(Vector3.forward, m_mousePosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_rotationSpeed);

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
}
