using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat combat;
    private bool hasCounteredAnybody;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        hasCounteredAnybody = combat.CounterAttackPerformed();
        Anim.SetBool("counterAttackPerformed", hasCounteredAnybody);

        stateTimer = combat.counterAttackRecoveryDuration;
    }

    public override void Update()
    {
        base.Update();

        HandleVelocity();
        

        if (animationTriggerCalled)
        {
            stateMachine.ChangeState(player.IdleState); 
        }

        if(stateTimer < 0 && !hasCounteredAnybody)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void HandleVelocity()
    {
        player.SetVelocity(0, 0);
    }
}
