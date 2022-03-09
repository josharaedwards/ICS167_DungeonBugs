// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour, TurnEventReciever
{
    private HashSet<AILogic> AIs;
    [SerializeField] private int count;

    private static AIManager instance;

    private GridManager gridManager;
    private PlayerManager playerManager;

    private TurnEventHandler turnEventHandler;

    public static AIManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        AIs = new HashSet<AILogic>();
        instance = this;
    }

    void Start()
    {
        gridManager = GridManager.GetInstance();
        playerManager = PlayerManager.GetInstance();

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);
    }

    void Update()
    {
        count = AIs.Count;
    }

    public void Add(AILogic AI) // Will be called by AILogics on Start()
    {
        AIs.Add(AI);
    }

    public void Remove(AILogic AI)
    {
        AIs.Remove(AI);
    }

    public GameObject GetTarget(AILogic AI) // return a valid target for the AI (target with the minimum distance for now)
    {
        float min_dist = float.MaxValue;
        float t_dist;
        GameObject target = null;
        Vector3Int AIPos = gridManager.GetPosFromObject(AI.gameObject);
        Vector3Int targetPos;

        foreach (GameObject player in playerManager.GetUnits())
        {
            targetPos = gridManager.GetPosFromObject(player);
            t_dist = Vector3Int.Distance(AIPos, targetPos);
            if (t_dist < min_dist)
            {
                min_dist = t_dist;
                target = player;
            }
        }

        return target;
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        if (turnState == GameManager.TurnState.EnemyTurn) // Tell each AI to do its moves
        {
            StartCoroutine(ExecuteTurn());
        }
    }

    IEnumerator ExecuteTurn()
    {
        ActionQueue actionQueue;
        float waitSeconds = 0.3f;
        foreach (AILogic AI in AIs)
        {
            AI.NextAction();
            actionQueue = AI.GetComponent<ActionQueue>();
            yield return StartCoroutine(actionQueue.Execute());
            yield return new WaitForSeconds(waitSeconds);
        }
        yield return StartCoroutine(EndTurn());
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);
        GameManager.GetInstance().ChangeTurnState();
    }

    IEnumerator DelayFrame(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return null;
        }
    }

    public HashSet<AILogic> GetAIList()
    {
        return AIs;
    }
}
