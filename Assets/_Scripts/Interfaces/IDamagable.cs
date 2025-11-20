using UnityEngine;

public interface IDamagable 
{
    public bool CanTakeDamage(float damage, Transform damageDealer);
}
