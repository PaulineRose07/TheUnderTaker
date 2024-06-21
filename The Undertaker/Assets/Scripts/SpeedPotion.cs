using UnityEngine;

public class SpeedPotion : PotionBase
{
    [SerializeField] private int m_speed;
    [SerializeField] private float m_speedTimer;


    public override void AddPowerUpToPlayer(PlayerMovements playerMovements)
    {
        playerMovements.AddSpeed(m_speed, m_speedTimer);
    }
}
