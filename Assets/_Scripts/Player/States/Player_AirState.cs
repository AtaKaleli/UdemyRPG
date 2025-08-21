public class Player_AirState : EntityState
{
    public Player_AirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleOnAirVelocity();

        if (player.Input.Player.Attack.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.JumpAttackState);
        }
    }

    private void HandleOnAirVelocity()
    {
        player.SetVelocity(player.MoveInput.x * (player.moveSpeed * player.onAirMultiplier), playerRb.linearVelocity.y);
    }
}
