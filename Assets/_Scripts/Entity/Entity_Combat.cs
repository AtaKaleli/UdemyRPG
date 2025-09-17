using UnityEngine;

public class Entity_Combat : MonoBehaviour
{


    [Header("Target Detection Data")]
    [SerializeField] private Transform detectionCheck;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask targetLayer;

    [Header("Damage Data")]
    [SerializeField] private float damageAmount;




    public void PerformAttack()
    {
        foreach (var collider in DetectedColliders())
        {
            Entity_Health targetHealth = collider.GetComponent<Entity_Health>();
            targetHealth?.TakeDamage(damageAmount,transform);
        }
    }

    private Collider2D[] DetectedColliders()
    {
        return Physics2D.OverlapCircleAll(detectionCheck.position, detectionRadius, targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectionCheck.position, detectionRadius);   
    }
}
