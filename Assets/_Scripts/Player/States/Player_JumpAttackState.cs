using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();


    }


    public override void Update()
    {
        base.Update();


        if (player.IsGroundDetected)
        {
            player.PlayerAnimation.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, playerRb.linearVelocity.y);
        }
        
        if (animationTriggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.PlayerAnimation.ResetTrigger("jumpAttackTrigger");
        
    }
}
