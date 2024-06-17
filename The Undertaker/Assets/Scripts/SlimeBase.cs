using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeBase : MonoBehaviour
{
    [SerializeField] public int m_damagesToPlayer;
    [SerializeField] public int m_lives;
    [SerializeField] public int m_pointsToScore;
    [SerializeField] public float m_speed;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    public GameManager m_gameManager;
    public PoolSystem m_poolSystem;

    public abstract void SplitWhenDead();
}
