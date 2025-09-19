using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, Transform damageProvider)
    {
        if(damageProvider.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageProvider);
        }


        base.TakeDamage(damage, damageProvider);
    }



}
