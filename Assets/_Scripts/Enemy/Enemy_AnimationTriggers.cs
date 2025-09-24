using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
    }

    private void EnableCounterAttackWindow()
    {
        enemy.SetCounterWindow(true);
    }
    
    private void DisableCounterAttackWindow()
    {
        enemy.SetCounterWindow(false);
    }
}
