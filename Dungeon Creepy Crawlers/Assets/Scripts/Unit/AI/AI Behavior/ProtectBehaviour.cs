using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI Behaviour/Protect Behaviour")]
public class ProtectBehaviour: BasicBehaviour
{
    [SerializeField] [Range(0f, 1f)] protected float guard_hp_threshold = 0.4f;
    [SerializeField] protected int guardRange = 15;

    public override AIState InitializeState()
    {
        wander = false;
        return AIState.Stop;
    }


    public override AIState NextAction(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        HashSet<AILogic> AIs = AIManager.GetInstance().GetAIList();
        HashSet<GameObject> guardCandidates = new HashSet<GameObject>();

        // Find candidates with HP below threshold
        foreach (AILogic AILogic in AIs)
        {
            stats t = AILogic.GetComponent<StatsTracker>().GetStats();
            int hp = t.hp;
            int maxHP = t.maxHP;
            if ((float)hp/maxHP <= guard_hp_threshold)
            {
                guardCandidates.Add(AILogic.gameObject);
            }
        }
        GridManager gridManager = GridManager.GetInstance();
        GameObject guardTarget = null;
        float minDistance = float.MaxValue;
        foreach (GameObject candidate in guardCandidates)   // Find closest candidate
        {
            float d = Distance(movementAI, candidate);
            if (d < minDistance)
            {
                minDistance = d;
                guardTarget = candidate;
            }
        }
        
        // Check if closest candidate in guardRange
        if (minDistance - 0.1f <= guardRange)
        {
            currentState = AIState.Guard;
            currentTarget = guardTarget;
            return Guard(AIStats, movementAI, abilityAI, currentState, currentTarget);
        }
        else
        {
            return base.NextAction(AIStats, movementAI, abilityAI, currentState, currentTarget);
        }
    }

    private AIState Guard(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget)
    {
        bool casted = abilityAI.CastNext(currentState, currentTarget);
        if (!casted)
        {
            movementAI.Pursue(currentTarget);
            casted = abilityAI.CastNext(currentState, currentTarget);
        }

        return AIState.Guard;
    }


}
