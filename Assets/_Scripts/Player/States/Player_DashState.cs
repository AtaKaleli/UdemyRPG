using System.Diagnostics;

public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashTimer;

    }

    public override void Update()
    {
        base.Update();

        HandleDash();

        if (player.IsWallDetected)
        {
            if (player.IsGroundDetected)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }

       
        if (stateTimer < 0)
        {
            if (player.IsGroundDetected)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.FallState);
            }
        }

    }

    private void HandleDash()
    {
        float dashDirection = player.MoveInput.x != 0 ? player.MoveInput.x : player.FacingDirection;
        player.SetVelocity(dashDirection * player.moveSpeed * player.dashMultiplier, 0);
    }



}
