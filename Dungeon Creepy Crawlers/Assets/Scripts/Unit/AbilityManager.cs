using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private static AbilityManager instance;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = GridManager.GetInstance();
    }

    private void Awake()
    {
        instance = this;
    }

    public static AbilityManager GetInstance() {
        return instance;
    }

    public bool InRange(StatsTracker caster, StatsTracker target, Ability ability) { // TODO
        return true;
    }

    public void Cast(StatsTracker caster, StatsTracker target, Ability ability) {
        if (InRange(caster, target, ability)) {
            ability.Execute(target);
        }
    }
}
