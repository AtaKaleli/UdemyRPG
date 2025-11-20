using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override bool CanTakeDamage(float damage, Transform damageProvider)
    {
        bool canTakeDamage = base.CanTakeDamage(damage, damageProvider);


        if (!canTakeDamage) return false;

        if (damageProvider.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageProvider);
        }


        return true;

    }



}
