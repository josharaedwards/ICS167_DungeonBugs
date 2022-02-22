using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Stop,
    Wander,
    Aggro,
}

public abstract class AIBehaviour: ScriptableObject
{
    [SerializeField] protected int aggroRange;


    protected bool inAggroRange(MovementAI movementAI, GameObject currentTarget)
    {
        GridManager gridManager = GridManager.GetInstance();
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(currentTarget);
        return (Vector3Int.Distance(movementAI.CurrentPos(), currentTargetPos) <= aggroRange);
    }

    public abstract AIState InitializeState();

    public abstract AIState NextAction(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget);
}
