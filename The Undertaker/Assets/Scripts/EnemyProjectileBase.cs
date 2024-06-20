using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBase : ProjectileBase
{
    [SerializeField] private float m_rotationZ = 45;
    [SerializeField] public Vector3 m_direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public new void ProjectileMovement()
    {
        transform.Translate(m_direction * Time.deltaTime * m_speedOfProjectile, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMovement();
        transform.Rotate(0,0,m_rotationZ *  Time.deltaTime);
    }


}
