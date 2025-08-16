using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D playerRb;

    protected float stateTimer;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        playerRb = player.PlayerRigidBody;
    }

 

    public virtual void Enter()
    {
        player.PlayerAnimation.SetBool(animBoolName, true);
        
    }

    public virtual void Update()
    {
        

        stateTimer -= Time.deltaTime;
        
        player.PlayerAnimation.SetFloat("yVelocity", playerRb.linearVelocity.y);

        if (player.Input.Player.Dash.WasPressedThisFrame() && player.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public virtual void Exit()
    {
        player.PlayerAnimation.SetBool(animBoolName, false);
  
        
    }

    




}
