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
    }

    public void NextAction()
    {
        currentTarget = manager.GetTarget(this);
        movementAI.EnableMovement();

        bool casted = abilityHandlerAI.CastNext(); // try casting ability as soon as possible
        movementAI.MoveNext(casted);
        if (!casted) // if ability has not been casted.
        {
            abilityHandlerAI.CastNext();
        }
    }
}
