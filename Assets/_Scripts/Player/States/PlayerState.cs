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

        Anim.SetFloat("yVelocity", Rb.linearVelocity.y);

        if (player.Input.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }



}
