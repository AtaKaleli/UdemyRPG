using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputSet Input { get; private set; }
    private StateMachine stateMachine;
 

    public  Animator PlayerAnimation { get; private set; }
    public Rigidbody2D PlayerRigidBody { get; private set; }


    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }



    [Header("Movement Data")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isFacingRight = true;
    public int FacingDirection { get; private set; } = 1;
    public Vector2 MoveInput { get; private set; }

    [Range(0, 1)]
    public float onAirMultiplier = 0.75f;
    [Range(0, 1)]
    public float wallSlideMultiplier = 0.4f;

    [Header("Wall Jump Data")]
    public Vector2 wallJumpVector = new Vector2(5, 5);

    [Header("Dash Data")]
    public float dashMultiplier;
    public float dashTimer;

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



    private void Awake()
    {
        PlayerAnimation = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();

        Input = new PlayerInputSet();
        stateMachine = new StateMachine();


        IdleState = new Player_IdleState(this, stateMachine, "idleState");
        MoveState = new Player_MoveState(this, stateMachine, "moveState");
        JumpState = new Player_JumpState(this, stateMachine, "jumpFallState");
        FallState = new Player_FallState(this, stateMachine, "jumpFallState");
        WallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlideState");
        WallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFallState");
        DashState = new Player_DashState(this, stateMachine, "dashState");
    }

    private void OnEnable()
    {
        Input.Enable();

        Input.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        Input.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
    }

    

    private void OnDisable()
    {
        Input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CollisionChecks();


        stateMachine.CurrentState.Update();

    }


    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRigidBody.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }


    private void CollisionChecks()
    {
        IsGroundDetected = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundDistance, groundLayer);
        IsWallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, wallDistance, wallLayer);
    }

    


    public void HandleFlip()
    {
        if (PlayerRigidBody.linearVelocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (PlayerRigidBody.linearVelocity.x > 0 && !isFacingRight)
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

    public bool CanDash()
    {
        if (IsWallDetected)
        {
            return false;
        }

        return true;
    }


    private void OnDrawGizmos()
    {
        
        Gizmos.DrawLine(wallCheckTransform.position, new Vector3(wallCheckTransform.position.x + (wallDistance * FacingDirection), wallCheckTransform.position.y));
        
        Gizmos.DrawLine(groundCheckTransform.position, new Vector3(groundCheckTransform.position.x, groundCheckTransform.position.y - groundDistance));
    }



}
