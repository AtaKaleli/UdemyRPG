using UnityEngine;

public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocity(0, Rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.M))
            stateMachine.ChangeState(enemy.MoveState);
    }
}
