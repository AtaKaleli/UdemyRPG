using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, Rb.linearVelocity.y);

    }

    public override void Update()
    {
        base.Update();

        if ((player.FacingDirection == player.MoveInput.x) && player.IsWallDetected)
            return;

        if (player.MoveInput.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }


    }
}
