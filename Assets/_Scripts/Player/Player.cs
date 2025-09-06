using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet Input { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }
    public Player_JumpAttackState JumpAttackState { get; private set; }




    [Header("Movement Data")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Vector2 MoveInput { get; private set; }

    [Range(0, 1)]
    public float onAirMultiplier = 0.75f;
    [Range(0, 1)]
    public float wallSlideMultiplier = 0.4f;


    [Header("Wall Jump Data")]
    public Vector2 wallJumpVector = new Vector2(5f, 5f);


    [Header("Dash Data")]
    public float dashMultiplier;
    public float dashTimer;


    [Header("Basic Attack Data")]
    public Vector2 attackVelocity = new Vector2(3f, 1.5f);
    public float attackVelocityTimer = 0.1f;
    public float comboResetTime = 2f;



    protected override void Awake()
    {
        base.Awake();

        Input = new PlayerInputSet();


        IdleState = new Player_IdleState(this, stateMachine, "idleState");
        MoveState = new Player_MoveState(this, stateMachine, "moveState");
        JumpState = new Player_JumpState(this, stateMachine, "jumpFallState");
        FallState = new Player_FallState(this, stateMachine, "jumpFallState");
        WallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlideState");
        WallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFallState");
        DashState = new Player_DashState(this, stateMachine, "dashState");
        BasicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttackState");
        JumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttackState");
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


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }
}
