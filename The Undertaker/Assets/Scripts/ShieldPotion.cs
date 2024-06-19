using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPotion : PotionBase
{
    [SerializeField] private int m_shieldTimer;
    public override void AddPowerUpToPlayer(PlayerMovements playerMovements)
    {
        playerMovements.AddShield(m_shieldTimer);
    }
}
