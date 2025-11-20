using UnityEngine;

public class Entity_StatSystem : MonoBehaviour
{

    public Stat maxHp;
    public Stat_MajorGroup majorStats;
    public Stat_OffenseGroup offensiveStats;
    public Stat_DefensiveGroup defensiveGroup;



    public float GetMaxHealth()
    {
        float baseHP = maxHp.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * 5;

        return baseHP + bonusHP;

        
    }


}
