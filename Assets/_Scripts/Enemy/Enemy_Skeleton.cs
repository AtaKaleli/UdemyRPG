using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, stateMachine, "idleState");
        MoveState = new Enemy_MoveState(this, stateMachine, "moveState");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }
}
