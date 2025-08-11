using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;



    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

 

    public virtual void Enter()
    {
        Debug.Log("Entered state: " + stateName);
    }

    public virtual void Update()
    {
        Debug.Log("Updated state: " + stateName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exited state: " + stateName);
    }





}
