using System;
using UnityEngine;

public class Player_WallSlideState : EntityState
{

   

    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
            player.Flip();
        }

        if (!player.IsWallDetected)
            stateMachine.ChangeState(player.FallState);

        HandleWallSlide();
    }

    private void HandleWallSlide()
    {
        if (player.MoveInput.y == -1)
            player.SetVelocity(player.MoveInput.x, playerRb.linearVelocity.y);
        else
            player.SetVelocity(player.MoveInput.x, playerRb.linearVelocity.y * player.wallSlideMultiplier);
    }
}
