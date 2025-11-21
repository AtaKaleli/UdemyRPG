using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    private Animator anim;
    private Rigidbody2D rb;
    private Entity_VFX entity_VFX;

    [SerializeField] private Vector2 velocityFeedbackVector;
    [SerializeField] private float randomAngularVelocity;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entity_VFX = GetComponentInChildren<Entity_VFX>();
    }

    public bool CanTakeDamage(float damage, Transform damageDealer)
    {
        entity_VFX.PlayOnTakeDamageTakenVFX();
        rb.linearVelocity = velocityFeedbackVector;
        rb.angularVelocity = Random.Range(-randomAngularVelocity, randomAngularVelocity);
        anim.SetTrigger("opened");

        return true;
    }



}
