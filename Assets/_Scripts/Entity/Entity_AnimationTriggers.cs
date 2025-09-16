using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entity_Combat;


    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entity_Combat = GetComponentInParent<Entity_Combat>();
    }





    private void CallAnimationTrigger()
    {
        entity.CallAnimationTrigger();
    }

    private void AttackTrigger()
    {
        entity_Combat.PerformAttack();
    }
}
