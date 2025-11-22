using UnityEngine;

public class Entity_StatSystem : MonoBehaviour
{
    [Header("Max HP")]
    public Stat maxHp;

    [Space(16)]
    [Header("Major Stats Releated Data")]
    public Stat_MajorStats majorStats;
    [SerializeField] private float vitalityHealthMultiplier = 5f;

    [Space(16)]
    [Header("Offensive Stats Releated Data")]
    public Stat_OffensiveStats offensiveStats;

    [Space(16)]
    [Header("Defensive Stats Releated Data")]
    public Stat_DefensiveStats defensiveStats;
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


    public float GetArmorMitigation(float opponentArmorReduction)
    {
        float baseArmor = defensiveStats.armor.GetValue();
        float bonusArmor = majorStats.vitality.GetValue();
        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp01(1 - opponentArmorReduction); // 1 - .4 = .6f that is used percetange of armor

        float finalArmor = totalArmor * reductionMultiplier;

        float armorMitigation = finalArmor / (finalArmor + 100); // 100 here represents scaling constant

        return Mathf.Clamp(armorMitigation, 0, .85f); // cap the mitigation so that player cant stack armor infinitely to become invictable
    }

    public float GetArmorReduction()
    {
        return  offensiveStats.armorReduction.GetValue() / 100; //total armor reduction  as multiplier
    }

    public float CalculateFinalDamage(float baseDamage, float armorMitigation)
    {
        return baseDamage * (1 - armorMitigation);
    }
    

    public float GetEvasion()
    {
        float baseEvasion = defensiveStats.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * agilityEvasionMultiplier; // each agility point gives you 0.5% of evasion
        float totalEvasion = baseEvasion + bonusEvasion;

        return Mathf.Clamp(totalEvasion, 0, evasionCap);
    }




}
