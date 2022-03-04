using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Stop,
    Wander,
    Aggro,
    Guard
}

public abstract class AIBehaviour: ScriptableObject
{
    [SerializeField] protected int aggroRange;


    protected float Distance(MovementAI movementAI, GameObject target)
    {
        GridManager gridManager = GridManager.GetInstance();
        Vector3Int targetPos = gridManager.GetPosFromObject(target);
        return Vector3Int.Distance(movementAI.CurrentPos(), targetPos);
    }


    protected bool inAggroRange(MovementAI movementAI, GameObject currentTarget)
    {
        return (Distance(movementAI, currentTarget) <= aggroRange);
    }

    public abstract AIState InitializeState();

    public abstract AIState NextAction(StatsTracker AIStats, MovementAI movementAI, AbilityHandlerAI abilityAI, AIState currentState, GameObject currentTarget);
}
