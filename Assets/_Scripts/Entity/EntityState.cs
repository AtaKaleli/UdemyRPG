using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D Rb;
    protected Animator Anim;

    protected float stateTimer;
    protected bool animationTriggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        animationTriggerCalled = false;
        Anim.SetBool(animBoolName, true);

    }

    public virtual void Update()
    {


        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }

    public virtual void Exit()
    {
        Anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        animationTriggerCalled = true;
    }

    public virtual void UpdateAnimationParameters()
    {

    }
}
