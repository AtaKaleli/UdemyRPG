using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 1; // we start our combo with index 1, it is used in animator
    private const int ComboLimit = 3; // number of attack combos we have

    private int basicAttackIndex = 1;

    private float lastAttackTime;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(ComboLimit != player.attackVelocity.Length)
        {
            Debug.LogError("Combo limit and attack velocity length are not same!");
        }
    }

    public override void Enter()
    {
        base.Enter();

        HandleAttackCombo();
        HandleAttackFeedback();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (animTrigger)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        basicAttackIndex++;
        
        

    }


    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0) // this way player can move in this tiny little time
        {
            player.SetVelocity(0, playerRb.linearVelocity.y);
        }
    }

    private void HandleAttackCombo()
    {
        if(Time.time > lastAttackTime + player.comboResetTime)
        {
            basicAttackIndex = FirstComboIndex;
        }

        if (basicAttackIndex > ComboLimit) // reset combo attack index if needed
        {
            basicAttackIndex = FirstComboIndex;
        }

        player.PlayerAnimation.SetInteger("basicAttackIndex", basicAttackIndex);
    }

    private void HandleAttackFeedback()
    {
        attackVelocityTimer = player.attackVelocityTimer;
        
        Vector2 attackVelocity = player.attackVelocity[basicAttackIndex - 1];
        player.SetVelocity(attackVelocity.x * player.FacingDirection, attackVelocity.y);
    }


}
