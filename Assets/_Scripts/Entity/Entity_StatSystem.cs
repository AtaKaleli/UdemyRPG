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
    [SerializeField] private float evasionCap = 85f;



    public float GetMaxHealth()
    {
        float baseHP = maxHp.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * vitalityHealthMultiplier;

        return baseHP + bonusHP;


    }

    public float GetPhysicalDamage()
    {
        float baseDamage = offensiveStats.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        bool isCritAttack = IsPerformedCritAttack();

        float baseCritPower = offensiveStats.critPower.GetValue();
        float bonusCritPower = majorStats.strength.GetValue() * 0.5f;
        float totalCritPower = baseCritPower + bonusCritPower;


        return isCritAttack ? totalBaseDamage * (totalCritPower / 100) : totalBaseDamage;
    }

    public bool IsPerformedCritAttack()
    {
        float baseCritChance = offensiveStats.critChance.GetValue();
        float bonusCritChance = majorStats.agility.GetValue() * 0.3f;
        float totalCritChance = baseCritChance + bonusCritChance;

        return Random.Range(0, 100) <= totalCritChance;
    }


    public float GetEvasion()
    {
        float baseEvasion = defensiveGroup.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * agilityEvasionMultiplier; // each agility point gives you 0.5% of evasion
        float totalEvasion = baseEvasion + bonusEvasion;

        return Mathf.Clamp(totalEvasion, 0, evasionCap);
    }




}
