using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter Attack Data")]
    public float counterAttackRecoveryDuration;

    public bool CounterAttackPerformed()
    {
        bool hasCounteredAnybody = false;

        foreach (var target in DetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null)
                continue;

            if(counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasCounteredAnybody = true;
            }
        }

        return hasCounteredAnybody;
    }

    

}
