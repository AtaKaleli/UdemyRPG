using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;


    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(player == null)
        {
            player = enemy.PlayerDetection().transform;
        }
        
    }

    public override void Update()
    {
        base.Update();

        if (IsWithinAttackRange())
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), Rb.linearVelocityY);
        }
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

}
