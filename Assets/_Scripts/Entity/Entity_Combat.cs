using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entity_VFX;

    [Header("Target Detection Data")]
    [SerializeField] private Transform detectionCheck;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask targetLayer;

    [Header("Damage Data")]
    [SerializeField] private float damageAmount;



    private void Awake()
    {
        entity_VFX = GetComponentInChildren<Entity_VFX>();
    }

    public void PerformAttack()
    {
        foreach (var target in DetectedColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue; // skip target, go to next one

            damagable.TakeDamage(damageAmount, transform);
            entity_VFX.CreateOnHitVFX(target.transform);
        }
    }

    protected Collider2D[] DetectedColliders()
    {
        return Physics2D.OverlapCircleAll(detectionCheck.position, detectionRadius, targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectionCheck.position, detectionRadius);   
    }
}
