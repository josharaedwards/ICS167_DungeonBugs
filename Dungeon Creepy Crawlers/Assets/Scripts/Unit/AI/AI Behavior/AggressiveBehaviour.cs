using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI Behaviour/Aggressive Behaviour")]
public class AggressiveBehaviour : AIBehaviour
{
    [SerializeField] private bool kite = false;



    public override AIState InitializeState()
    {
        return AIState.Aggro;
    }

    public override AIState NextAction(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        bool casted = abilityAI.CastNext();
        if (!casted)
        {
            movementAI.Pursue();
            casted = abilityAI.CastNext();
        }
        if (casted && kite)
        {
            movementAI.RunAway();
        }

        return AIState.Aggro;
    }
}
