using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandlerAI : AbilityHandler
{
    private AbilityManager abilityManager;

    private AILogic mainLogic;
    private ActionQueue actionQueue;

    private GameObject currentTarget;
    private Ability currentAbility;
    

    protected override void Start()
    {
        base.Start();

        abilityManager = AbilityManager.GetInstance();
        mainLogic = GetComponent<AILogic>();
        actionQueue = GetComponent<ActionQueue>();
    }

    public (Ability, bool) CastNext(AIState currentState, GameObject target)
    {
        currentAbility = GetNextAbility();
        currentTarget = target;
        bool result = abilityManager.ValidCast(statsTracker, target.GetComponent<StatsTracker>(), currentAbility);
        if (result)
        {
            actionQueue.Add(CastCoroutine);
        }
        return (currentAbility, result);
    }

    private IEnumerator CastCoroutine()
    {
        float waitTime = 0.2f;
        yield return new WaitForSeconds(waitTime);
        abilityManager.Cast(statsTracker, currentTarget.GetComponent<StatsTracker>(), currentAbility);
        yield return null;
    }

    //TODO: Logic in choosing ability / possibly some finite state machine
    private Ability GetNextAbility() // just return the first ability for now
    {
        return abilities[0];
    }
}
