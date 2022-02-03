// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogic : MonoBehaviour
{
    private AIManager manager;

    private GameObject currentTarget;

    private MovementAI movementAI;
    // Will have script component for abilityAI


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
    }

    public void NextAction()
    {
        if (currentTarget == null)
        {
            currentTarget = manager.GetTarget(this);
        }
        movementAI.MoveNext();
        currentTarget = manager.GetTarget(this);
    }
}
