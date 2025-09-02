using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.JumpState);
        }

        if (player.Input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.BasicAttackState);
        }


        if (player.Rb.linearVelocity.y < 0 && !player.IsGroundDetected)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}
