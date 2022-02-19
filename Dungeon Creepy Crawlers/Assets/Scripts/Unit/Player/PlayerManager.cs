// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, TurnEventReciever
{
    private HashSet<GameObject> players;
    //[SerializeField] private int count;


    private static PlayerManager instance;

    [SerializeField] private int maxActionPoint; // shared pool of AP for the party
    [SerializeField] private int refillActionPoint;

    private Dictionary<GameManager.TurnState, TeamManager> teamManagers;


    private int currentActionPoint;

    private TurnEventHandler turnEventHandler;

    private AbilityManager abilityManager;



    public static PlayerManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("NULL PlayerManager");
        }
        return instance;
    }

    void Awake()
    {
        players = new HashSet<GameObject>();

        teamManagers = new Dictionary<GameManager.TurnState, TeamManager>();

        teamManagers.Add(GameManager.TurnState.Player1Turn, new TeamManager(maxActionPoint, refillActionPoint));
        teamManagers.Add(GameManager.TurnState.Player2Turn, new TeamManager(maxActionPoint, refillActionPoint));

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
        //count = players.Count;
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState) // Refill AP every player turn
    {
        if (turnState == GameManager.TurnState.EnemyTurn)
        {
            return;
        }
        teamManagers[turnState].RefillActionPoint();
    }

    public HashSet<GameObject> GetUnits()
    {
        return players;
    }

    public void Add(StatsTracker unit) // Will be called by players' StatTracker on Start()
    {
        players.Add(unit.gameObject);
        teamManagers[unit.GetComponent<TurnEventHandler>().turn].Add(unit);

    }

    public void Remove(StatsTracker unit)
    {
        players.Remove(unit.gameObject);
        teamManagers[unit.GetComponent<TurnEventHandler>().turn].Remove(unit);
    }

    public bool Cast(StatsTracker caster, StatsTracker target, Ability ability) // Will be called by indivdual player unit to execute ability
    {
        /*if (ability.apCost <= currentActionPoint)
        {
            bool result = abilityManager.Cast(caster, target, ability);
            if (result)
            {
                currentActionPoint -= ability.apCost;
            }
            return result;
        }*/

        bool result = teamManagers[caster.GetComponent<TurnEventHandler>().turn].Cast(caster, target, ability);

        return result;
    }

    public int GetCurrentActionPoint(GameManager.TurnState turnState = GameManager.TurnState.Player1Turn)
    {
        return teamManagers[turnState].GetCurrentActionPoint();
    }

    public int GetMaxActionPoint(GameManager.TurnState turnState = GameManager.TurnState.Player1Turn)
    {
        return teamManagers[turnState].GetMaxActionPoint();
    }
}
