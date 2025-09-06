using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HandleWallJump();

    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected)
            stateMachine.ChangeState(player.WallSlideState);


        if (Rb.linearVelocity.y < 0.01f)
            stateMachine.ChangeState(player.FallState);
        
        

    }

    private void HandleWallJump()
    {
        
        int jumpDirection = player.FacingDirection * -1;
        player.SetVelocity(player.wallJumpVector.x * jumpDirection, player.wallJumpVector.y);

 

    }
}
