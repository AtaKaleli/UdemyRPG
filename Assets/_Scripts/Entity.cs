using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;


    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }


    private bool isFacingRight = true;
    public int FacingDirection { get; private set; } = 1;






    [Header("Collision Check - Ground")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool IsGroundDetected { get; private set; }

    [Header("Collision Check - Wall")]
    [SerializeField] private Transform wallCheckTransform;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask wallLayer;
    public bool IsWallDetected { get; private set; }



    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();

        
    }



    protected virtual void Start()
    {

    }



    private void Update()
    {
        CollisionChecks();


        stateMachine.CurrentState.Update();

    }


    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }


    private void CollisionChecks()
    {
        IsGroundDetected = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundDistance, groundLayer);
        IsWallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, wallDistance, wallLayer);

    }




    public void HandleFlip()
    {
        if (Rb.linearVelocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (Rb.linearVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }

    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        FacingDirection = FacingDirection * -1;
        transform.Rotate(0f, 180f, 0f);
    }


    public void CallAnimationTrigger()
    {
        stateMachine.CurrentState.CallAnimationTrigger();
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(wallCheckTransform.position, new Vector3(wallCheckTransform.position.x + (wallDistance * FacingDirection), wallCheckTransform.position.y));

        Gizmos.DrawLine(groundCheckTransform.position, new Vector3(groundCheckTransform.position.x, groundCheckTransform.position.y - groundDistance));
    }



}
