
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour,IDamagable
{
    private Entity entity;
    private Entity_VFX entity_VFX;
    private Entity_StatSystem entity_Stat;

    private Slider healthBar;


    [Header("HP Data")]
    public float currentHp;
    public bool isDead;

    [Header("On Damage Knockback Data")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(3f, 2f);
    [SerializeField] private float knockbackDuration = 0.15f;

    [Header("On Heavy Damage Knockback Data")]
    [SerializeField] private float heavyDamageThreshold = 0.3f; // Percentage of health you should lose to consider damage as heavy

    [Range(1.5f, 2.5f)]
    [SerializeField] private float knockbackPowerMultiplier;
    [Range(1.5f, 2.5f)]
    [SerializeField] private float knockbackDurationMultiplier;



    private void Awake()
    {
        entity = GetComponent<Entity>();
        entity_VFX = GetComponentInChildren<Entity_VFX>();
        entity_Stat = GetComponent<Entity_StatSystem>();
        healthBar = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        currentHp = entity_Stat.GetMaxHealth();
        UpdateHealthBar();
    }


    public virtual bool CanTakeDamage(float damage, Transform damageProvider)
    {

        if (isDead || IsAttackAvoided()) return false;

        Entity_StatSystem damageProviderStats = damageProvider.GetComponent<Entity_StatSystem>();

        //armor reduction of opponent, used in calculation of armor mitigation of target that took damage.
        float armorReduction = damageProviderStats != null ? damageProviderStats.GetArmorReduction() : 0;
        float armorMitigation = entity_Stat.GetArmorMitigation(armorReduction);

        float finalDamage = entity_Stat.CalculateFinalDamage(damage, armorMitigation);


        entity?.ReceiveKnockback(CalculateKnockbackPower(knockbackPower, finalDamage),
            CalculateKnockbackDirection(damageProvider),
            CalculateKnockbackDuration(knockbackDuration, finalDamage));

        entity_VFX?.PlayOnTakeDamageTakenVFX();

        Debug.Log("I took " + finalDamage + " damage");
        ReduceHP(finalDamage);

        return true;
    }

    private bool IsAttackAvoided() => Random.Range(0, 100) <= entity_Stat.GetEvasion();


    private void ReduceHP(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;

        healthBar.value = currentHp / entity_Stat.GetMaxHealth();
    }

    private bool HasReceivedHeavyDamage(float damage) => damage > entity_Stat.GetMaxHealth() * heavyDamageThreshold;
    private Vector2 CalculateKnockbackPower(Vector2 knockbackPower, float damage) => HasReceivedHeavyDamage(damage) ? knockbackPower * knockbackPowerMultiplier : knockbackPower;
    private float CalculateKnockbackDuration(float knockbackDuration, float damage) => HasReceivedHeavyDamage(damage) ? knockbackDuration * knockbackDurationMultiplier : knockbackDuration;
    private int CalculateKnockbackDirection(Transform damageProvider) => damageProvider.position.x > transform.position.x ? -1 : 1;
}
