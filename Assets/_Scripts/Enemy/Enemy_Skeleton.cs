using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    



    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, stateMachine, "idleState");
        MoveState = new Enemy_MoveState(this, stateMachine, "moveState");
        AttackState = new Enemy_AttackState(this, stateMachine, "attackState");
        BattleState = new Enemy_BattleState(this, stateMachine, "battleState");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }
}
