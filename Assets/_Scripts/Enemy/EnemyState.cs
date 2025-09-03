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
}
