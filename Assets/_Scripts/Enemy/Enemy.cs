using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState IdleState;
    public Enemy_MoveState MoveState;
    public Enemy_AttackState AttackState;
    public Enemy_BattleState BattleState;

    [Header("Movement Data")]
    public float moveSpeed;
    public float idleTime;

    [Range(0,2)]
    public float moveSpeedMultiplier;

    [Header("Player Detection Data")]
    public Transform playerCheck;
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;
    private const string PlayerLayer = "Player";

    [Header("Battle Data")]
    public float battleMoveSpeed;
    public float attackDistance;







    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D hit = 
            Physics2D.Raycast(playerCheck.position, Vector2.right * FacingDirection, playerCheckRadius, whatIsPlayer | groundLayer);

  

        if(hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer(PlayerLayer))
        {
            return default; 
        }

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (FacingDirection * playerCheckRadius), playerCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (FacingDirection * attackDistance), playerCheck.position.y));
    }


}
