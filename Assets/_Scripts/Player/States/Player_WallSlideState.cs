using System;
using UnityEngine;

public class Player_WallSlideState : PlayerState
{

   

    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }



    public override void Update()
    {
        base.Update();
        HandleWallSlide();
        
        if (player.Input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.WallJumpState);

        if (player.IsGroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
            player.Flip();
        }

        if (!player.IsWallDetected)
            stateMachine.ChangeState(player.FallState);


    }

    private void HandleWallSlide()
    {
        if (player.MoveInput.y == -1)
            player.SetVelocity(player.MoveInput.x, Rb.linearVelocity.y);
        else
            player.SetVelocity(player.MoveInput.x, Rb.linearVelocity.y * player.wallSlideMultiplier);
    }
}
