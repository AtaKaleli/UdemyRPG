using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy; // for state changes

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        Anim = enemy.Anim;
        Rb = enemy.Rb;
    }

    public override void Update()
    {
        base.Update();

        Anim.SetFloat("moveSpeedMultiplier", enemy.moveSpeedMultiplier);
        Anim.SetFloat("xVelocity", Rb.linearVelocityX);
    }
}
