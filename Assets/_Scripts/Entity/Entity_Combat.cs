using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entity_VFX;
    private Entity_StatSystem entity_Stat;

    [Header("Target Detection Data")]
    [SerializeField] private Transform detectionCheck;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask targetLayer;


    private void Awake()
    {
        entity_VFX = GetComponentInChildren<Entity_VFX>();
        entity_Stat = GetComponent<Entity_StatSystem>();
    }

    public void PerformAttack()
    {
        foreach (var target in DetectedColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                continue; // skip target, go to next one

            bool canGiveDamage = damagable.CanTakeDamage(entity_Stat.GetPhysicalDamage(), transform);

            if (canGiveDamage)
            {
                entity_VFX.CreateOnHitVFX(target.transform, entity_Stat.IsPerformedCritAttack());
            }
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
