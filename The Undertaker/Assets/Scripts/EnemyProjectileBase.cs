using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBase : ProjectileBase
{
    [SerializeField] private float m_rotationZ = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,m_rotationZ *  Time.deltaTime);
    }
}
