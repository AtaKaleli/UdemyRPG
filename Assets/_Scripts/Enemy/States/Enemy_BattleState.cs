using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeInBattle;

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(player == null)
        {
            player = enemy.IsPlayerDetected().transform;
        }

        if (ShouldRetreat())
        {
            Rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
        }
        
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            UpdateBattleTimer();
        }

        if (IsStopChasing())
        {
            stateMachine.ChangeState(enemy.IdleState);
        }

        if (IsWithinAttackRange() && enemy.IsPlayerDetected() )
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), Rb.linearVelocityY);
        }
    }

    private void UpdateBattleTimer()
    {
        lastTimeInBattle = Time.time;
    }

    private bool IsWithinAttackRange() => enemy.attackDistance > DistanceToPlayer();

    private float DistanceToPlayer()
    {
        if(player == null) // if no player , then this means enemy is too far to player
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > enemy.transform.position.x  ? 1 : -1;
    }

    private bool IsStopChasing() => Time.time > lastTimeInBattle + enemy.battleTimeDuration;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;


}
