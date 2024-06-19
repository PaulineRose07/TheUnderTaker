using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : PotionBase
{
    [SerializeField] private int m_LifePoints;
    public override void AddPowerUpToPlayer()
    {
        m_gameManager.IncreaseLives(m_LifePoints);
    }

}
