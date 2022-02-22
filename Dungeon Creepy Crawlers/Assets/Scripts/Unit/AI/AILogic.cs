// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogic : MonoBehaviour
{
    private AIManager manager;

    private GameObject currentTarget;

    private MovementAI movementAI;
    private AbilityHandlerAI abilityHandlerAI;
    private StatsTracker statsTracker;

    private AIState currentState;

    [SerializeField] private AIBehaviour behaviour;


    public bool HasTarget()
    {
        return currentTarget != null;
    }

    public void SetTarget(GameObject target)
    {
        currentTarget = target;
    }

    public GameObject CurrentTarget()
    {
        return currentTarget;
    }
    void Start()
    {
        manager = AIManager.GetInstance();
        manager.Add(this);

        movementAI = GetComponent<MovementAI>();
        abilityHandlerAI = GetComponent<AbilityHandlerAI>();
        statsTracker = GetComponent<StatsTracker>();

        currentState = behaviour.InitializeState();
    }

    //TODO: Add Target choosing logic?
    public void NextAction()
    {
        currentTarget = manager.GetTarget(this);
        movementAI.EnableMovement();

        currentState = behaviour.NextAction(statsTracker, movementAI, abilityHandlerAI, currentState, currentTarget);
    }

    private void OnDisable()
    {
        manager.Remove(this);
    }
}
