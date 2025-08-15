using UnityEngine;

public class Player_GroundedState : EntityState
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

        if (player.PlayerRigidBody.linearVelocity.y < 0 && !player.IsGroundDetected)
            stateMachine.ChangeState(player.FallState);
    }
}
