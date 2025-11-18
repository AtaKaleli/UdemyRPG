using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        

        Anim.enabled = false;


        enemy.GetComponent<Collider2D>().enabled = false;
        Rb.linearVelocity = new Vector2(Rb.linearVelocityX, 15f);
        Rb.gravityScale = 12;
    }


}
