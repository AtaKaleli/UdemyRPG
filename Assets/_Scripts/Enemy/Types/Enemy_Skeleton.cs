using UnityEngine;

public class Enemy_Skeleton : Enemy, ICounterable
{

    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, stateMachine, "idleState");
        MoveState = new Enemy_MoveState(this, stateMachine, "moveState");
        AttackState = new Enemy_AttackState(this, stateMachine, "attackState");
        BattleState = new Enemy_BattleState(this, stateMachine, "battleState");
        DeadState = new Enemy_DeadState(this, stateMachine, "deadState");
        StunnedState = new Enemy_StunnedState(this, stateMachine, "stunnedState");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.M))
            HandleCounter();
    }
    public void HandleCounter()
    {
        if (!canBeStunnned)
            return;

        stateMachine.ChangeState(StunnedState);
    }
}
