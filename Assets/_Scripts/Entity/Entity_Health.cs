using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    private Entity_VFX entity_VFX;

    private void Awake()
    {
        entity_VFX = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        entity_VFX?.OnDamageFlashMaterial();
        ReduceHp(damage);
    }

    private void ReduceHp(float damage)
    {
        maxHp -= damage;

        if(maxHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}
