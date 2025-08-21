using UnityEngine;

public class Player_MoveState : Player_GroundedState
{



    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        

        if (player.MoveInput.x == 0 || player.IsWallDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        player.SetVelocity(player.MoveInput.x * player.moveSpeed, playerRb.linearVelocity.y);
    }

    



}
