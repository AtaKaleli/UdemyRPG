using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action OnFlipped;

    protected StateMachine stateMachine;
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }


    private bool isFacingRight = true;
    public int FacingDirection { get; private set; } = 1;






    [Header("Collision Check - Ground")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundDistance;
    public LayerMask groundLayer;
    public bool IsGroundDetected { get; private set; }

    [Header("Collision Check - Wall")]
    [SerializeField] private Transform wallCheckTransform;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask wallLayer;
    public bool IsWallDetected { get; private set; }


    //condition variables
    private bool isKnocked;
    private Coroutine knockbackCo;





    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();


    }



    protected virtual void Start()
    {

    }



    protected virtual void Update()
    {
        CollisionChecks();


        stateMachine.CurrentState.Update();

    }

    public virtual void EntityDeath()
    {

    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked) return;

        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }


    private void CollisionChecks()
    {
        IsGroundDetected = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundDistance, groundLayer);
        IsWallDetected = Physics2D.Raycast(wallCheckTransform.position, Vector2.right * FacingDirection, wallDistance, wallLayer);

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

        OnFlipped?.Invoke();
    }


    public void CallAnimationTrigger()
    {
        stateMachine.CurrentState.CallAnimationTrigger();
    }

    protected virtual void OnDrawGizmos()
    {

        Gizmos.DrawLine(wallCheckTransform.position, new Vector3(wallCheckTransform.position.x + (wallDistance * FacingDirection), wallCheckTransform.position.y));

        Gizmos.DrawLine(groundCheckTransform.position, new Vector3(groundCheckTransform.position.x, groundCheckTransform.position.y - groundDistance));
    }


    public void ReceiveKnockback(Vector2 knockback, int knockbackDirection, float duration)
    {
        if (knockbackCo != null)
        {
            StopCoroutine(knockbackCo);
        }

        knockbackCo = StartCoroutine(KnockbackCoroutine(knockback, knockbackDirection, duration));
    }

    private IEnumerator KnockbackCoroutine(Vector2 knockback, int knockbackDirection, float duration)
    {
        isKnocked = true;
        Rb.linearVelocity = new Vector2(knockback.x * knockbackDirection, knockback.y);
        yield return new WaitForSeconds(duration);
        Rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }




}
