using System;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] private float maxHP;

    public bool isDead;

    public virtual void TakeDamage(float damage, Transform damageProvider)
    {
        if (isDead) return;

        ReduceHP(damage);
    }

    private void ReduceHP(float damage)
    {
        maxHP -= damage;

        if(maxHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}
