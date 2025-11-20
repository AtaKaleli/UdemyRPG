using UnityEngine;

public class Entity_StatSystem : MonoBehaviour
{
    [Header("Max HP")]
    public Stat maxHp;

    [Space(16)]
    [Header("Major Stats Releated Data")]
    public Stat_MajorGroup majorStats;
    [SerializeField] private float vitalityHealthMultiplier = 5f;

    [Space(16)]
    [Header("Offensive Stats Releated Data")]
    public Stat_OffenseGroup offensiveStats;

    [Space(16)]
    [Header("Defensive Stats Releated Data")]
    public Stat_DefensiveGroup defensiveGroup;
    [SerializeField] private float agilityEvasionMultiplier = 0.5f;
    [SerializeField] private float evasionCap;



    public float GetMaxHealth()
    {
        float baseHP = maxHp.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * vitalityHealthMultiplier;

        return baseHP + bonusHP;

        
    }

    public float GetEvasion()
    {
        float baseEvasion = defensiveGroup.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * agilityEvasionMultiplier; // each agility point gives you 0.5% of evasion
        float totalEvasion = baseEvasion + bonusEvasion;

        return Mathf.Clamp(totalEvasion, 0, evasionCap);
    }


}
