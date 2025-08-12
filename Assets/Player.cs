using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;
    private FlipController flipController;

    public  Animator PlayerAnimation { get; private set; }
    public Rigidbody2D PlayerRigidBody { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    public Vector2 MoveInput { get; private set; }


    [Header("Movement Data")]
    public float moveSpeed = 5f;
    


    private void Awake()
    {
        PlayerAnimation = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();

        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        flipController = GetComponentInChildren<FlipController>();

        IdleState = new Player_IdleState(this, stateMachine, "idleState");
        MoveState = new Player_MoveState(this, stateMachine, "moveState");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
    }

    

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.UpdateCurrentState();

    }


    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRigidBody.linearVelocity = new Vector2(xVelocity, yVelocity);
        flipController.HandleFlip(xVelocity);
    }

    






}
