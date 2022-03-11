using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI Behaviour/Basic Behaviour")]
public class BasicBehaviour : AIBehaviour
{
    [SerializeField] protected bool wander = false;
    [SerializeField] protected bool kite = false;
    [SerializeField] protected bool aggressive = false; // if true never leaves aggro state
    [SerializeField] [Range(0f, 1f)] protected float p_stop_to_wander = 0.5f;
    [SerializeField] [Range(0f, 1f)] protected float p_wander_to_stop = 0.5f;


    public override AIState InitializeState()
    {
        if (!wander)
        {
            return AIState.Stop;
        }
        else
        {
            return AIState.Wander;
        }      
    }


    public override AIState NextAction(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        if (inAggroRange(movementAI, currentTarget))
        {
            currentState = AIState.Aggro;
        }
        else if (currentState == AIState.Aggro && !aggressive)
        {
            currentState = AIState.Stop;
        }

        switch (currentState)
        {
            case AIState.Stop:
                currentState = Stop(AIStats, movementAI, abilityAI, currentState, currentTarget);
                break;
            case AIState.Wander:
                currentState = Wander(AIStats, movementAI, abilityAI, currentState, currentTarget);
                break;
            case AIState.Aggro:
                currentState = Aggro(AIStats, movementAI, abilityAI, currentState, currentTarget);
                break;
        }

        return currentState;
    }


    private AIState Stop(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        if (wander)
        {
            float p = Random.Range(0f, 1f);
            if (p <= p_stop_to_wander)
            {
                return AIState.Wander;
            }
        }
        return currentState;
    }

    private AIState Wander(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        movementAI.Wander();

        float p = Random.Range(0f, 1f);
        if (p <= p_wander_to_stop)
        {
            return AIState.Stop;
        }
        return currentState;
    }

    private AIState Aggro(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        (Ability ability, bool result) casted = abilityAI.CastNext(currentState, currentTarget);
        if (!casted.result)
        {
            movementAI.Pursue(currentTarget, casted.ability.minRange);
            casted = abilityAI.CastNext(currentState, currentTarget);
        }
        if (casted.result && kite)
        {
            movementAI.RunAway(currentTarget);
        }
        return AIState.Aggro;
    }

}
