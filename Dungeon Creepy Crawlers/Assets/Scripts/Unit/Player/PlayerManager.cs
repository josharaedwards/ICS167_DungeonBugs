using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private HashSet<GameObject> players;

    private static PlayerManager instance;

    [SerializeField] private int ActionPoint; // shared pool of AP for the party



    public static PlayerManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        players = new HashSet<GameObject>();
        instance = this;
    }

    void Start()
    {

    }

    public HashSet<GameObject> GetUnits()
    {
        return players;
    }

    public void Add(StatsTracker unit) // Will be called by players' StatTracker on Start()
    {
        players.Add(unit.gameObject);
    }

    public bool ExecuteAbility(Ability ability, StatsTracker caster, StatsTracker target) // Will be called by indivdual player unit to execute ability
    {
        if (ability.apCost < ActionPoint)
        {
            // AbilityManager.Execute
        }

        return false;
    }

    public int GetActionPoint()
    {
        return ActionPoint;
    }
}
