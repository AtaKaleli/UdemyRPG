using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(Rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (player.Rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.FallState);

        
    }
}
