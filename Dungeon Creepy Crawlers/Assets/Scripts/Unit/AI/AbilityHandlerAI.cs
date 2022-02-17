using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandlerAI : AbilityHandler
{
    private AbilityManager abilityManager;

    private AILogic mainLogic;
    private Ability[] abilities;
    private StatsTracker statsTracker;


    protected override void Start()
    {
        base.Start();

        abilityManager = AbilityManager.GetInstance();

        mainLogic = GetComponent<AILogic>();
        statsTracker = GetComponent<StatsTracker>();
        abilities = statsTracker.abilities;
    }

    public bool CastNext()
    {
        Ability nextAbility = GetNextAbility();
        bool result = abilityManager.Cast(statsTracker, mainLogic.CurrentTarget().GetComponent<StatsTracker>(), nextAbility);
        return result;
    }

    //TODO: Logic in choosing ability / possibly some finite state machine
    private Ability GetNextAbility() // just return the first ability for now
    {
        return abilities[0];
    }
}