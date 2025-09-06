using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;

    protected PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        Rb = player.Rb;
        Anim = player.Anim;
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        Anim.SetFloat("yVelocity", Rb.linearVelocity.y);
    }


}
