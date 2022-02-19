using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager
{
    private HashSet<GameObject> players;

    [SerializeField] private int maxActionPoint; // shared pool of AP for the party
    [SerializeField] private int refillActionPoint;

    private int currentActionPoint;

    private AbilityManager abilityManager;

    public TeamManager(int maxActionPoint_, int refillActionPoint_)
    {
        maxActionPoint = maxActionPoint_;
        refillActionPoint = refillActionPoint_;

        abilityManager = AbilityManager.GetInstance();

        currentActionPoint = maxActionPoint;

        players = new HashSet<GameObject>();
    }

    public void RefillActionPoint() // Refill AP every player turn
    {
        if (currentActionPoint < maxActionPoint)
        {
            currentActionPoint = Mathf.Min(currentActionPoint + refillActionPoint, maxActionPoint);
        }
    }

    public HashSet<GameObject> GetUnits()
    {
        return players;
    }

    public void Add(StatsTracker unit) // Will be called by players' StatTracker on Start()
    {
        players.Add(unit.gameObject);
    }

    public void Remove(StatsTracker unit)
    {
        players.Remove(unit.gameObject);
    }

    public bool Cast(StatsTracker caster, StatsTracker target, Ability ability) // Will be called by indivdual player unit to execute ability
    {
        if (ability.apCost <= currentActionPoint)
        {
            bool result = abilityManager.Cast(caster, target, ability);
            if (result)
            {
                currentActionPoint -= ability.apCost;
            }
            return result;
        }

        return false;
    }

    public int GetCurrentActionPoint()
    {
        return currentActionPoint;
    }

    public int GetMaxActionPoint()
    {
        return maxActionPoint;
    }
}
