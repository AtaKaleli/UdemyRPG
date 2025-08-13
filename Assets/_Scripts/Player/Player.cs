using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputSet Input { get; private set; }
    private StateMachine stateMachine;
    private FlipController flipController;

    public  Animator PlayerAnimation { get; private set; }
    public Rigidbody2D PlayerRigidBody { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }

    public Vector2 MoveInput { get; private set; }


    [Header("Movement Data")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Range(0, 1)]
    public float onAirMultiplier = 0.75f;

    [Header("Collision Check - Ground")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool IsGroundDetected { get; private set; }


    private void Awake()
    {
        PlayerAnimation = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();

        Input = new PlayerInputSet();
        stateMachine = new StateMachine();
        flipController = GetComponentInChildren<FlipController>();

        IdleState = new Player_IdleState(this, stateMachine, "idleState");
        MoveState = new Player_MoveState(this, stateMachine, "moveState");
        JumpState = new Player_JumpState(this, stateMachine, "jumpFallState");
        FallState = new Player_FallState(this, stateMachine, "jumpFallState");
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
        GroundCollisionCheck();

        stateMachine.CurrentState.Update();

    }


    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRigidBody.linearVelocity = new Vector2(xVelocity, yVelocity);
        flipController.HandleFlip(xVelocity);
    }


    private void GroundCollisionCheck()
    {
        IsGroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundDistance));
    }



}
