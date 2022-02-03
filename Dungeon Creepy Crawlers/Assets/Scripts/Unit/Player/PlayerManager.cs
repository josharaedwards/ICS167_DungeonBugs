// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, TurnEventReciever
{
    private HashSet<GameObject> players;
    [SerializeField] private int count;

    private static PlayerManager instance;

    [SerializeField] private int maxActionPoint; // shared pool of AP for the party
    [SerializeField] private int refillActionPoint;

    private int currentActionPoint;

    private TurnEventHandler turnEventHandler;

    private AbilityManager abilityManager;



    public static PlayerManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("NULLLL");
        }
        return instance;
    }

    void Awake()
    {
        players = new HashSet<GameObject>();
        instance = this;
    }

    void Start()
    {
        abilityManager = AbilityManager.GetInstance();


        turnEventHandler = GetComponent<TurnEventHandler>();
        
        if (turnEventHandler)
        {
            turnEventHandler.Subscribe(this);
        }
        else
        {
            Debug.Log("ERROR: Missing Turn Event Handler");
        }

        currentActionPoint = maxActionPoint;
        
    }

    void Update()
    {
        count = players.Count;
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState) // Refill AP every player turn
    {
        if (!(turnState == turnEventHandler.turn))
        {
            return;
        }
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
