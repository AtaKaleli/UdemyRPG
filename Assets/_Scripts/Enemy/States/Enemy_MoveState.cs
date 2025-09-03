using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        HandleMovement();

        if (!enemy.IsGroundDetected || enemy.IsWallDetected)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    private void HandleMovement()
    {
        Anim.SetFloat("moveSpeedMultiplier", enemy.moveSpeedMultiplier);
        enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDirection, Rb.linearVelocityY);
    }
}
