using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private int basicAttackIndex = 1;

    private const int FirstAttackIndex = 1; // this is the index of the first attack used in animator
    private const int ComboAttackAmount = 3; // this is the number of attack we use in a combo

    private float lastAttackTime;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIfNeeded();
        SetAttackPushFeedback();

        player.PlayerAnimation.SetInteger("basicAttackIndex", basicAttackIndex);
    }


    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (animationTriggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }


    private void SetAttackPushFeedback()
    {
        attackVelocityTimer = player.attackVelocityTimer;
        player.SetVelocity(player.attackVelocity.x * player.FacingDirection, player.attackVelocity.y);
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0)
        {
            player.SetVelocity(0, playerRb.linearVelocity.y);
        }
    }


    public override void Exit()
    {
        base.Exit();

        basicAttackIndex++;
        lastAttackTime = Time.time;
        

    }

    private void ResetComboIfNeeded()
    {

        if(basicAttackIndex > ComboAttackAmount)
        {
            basicAttackIndex = FirstAttackIndex;
        }

        if(Time.time > lastAttackTime + player.comboResetTime)
        {
            basicAttackIndex = FirstAttackIndex;
        }
    }
    


}
