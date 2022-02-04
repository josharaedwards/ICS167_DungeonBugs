// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ongoing,
        Won,
        Lost
    }

    public enum TurnState
    {
        Neutral,
        PlayerTurn,
        EnemyTurn
    }

    private GameState gameState;
    private TurnState turnState;

    private HashSet<TurnEventHandler> turnHandlers;

    private HashSet<GameObject> playerObjects;
    private HashSet<GameObject> enemyObjects;


    private static GameManager instance; // SINGLETON
    public static GameManager GetInstance()
    {
        return instance;
    }


    void Awake()
    {
        instance = this;

        turnHandlers = new HashSet<TurnEventHandler>();

        playerObjects = new HashSet<GameObject>();
        enemyObjects = new HashSet<GameObject>();
    }

    void Start()
    {
        turnState = TurnState.PlayerTurn;
        //Debug.Log("PlAYER TURN");
    }

    public void Subscribe(TurnEventHandler handler)
    {
        turnHandlers.Add(handler);

        if (handler.gameObject.tag == "Player")
        {
            playerObjects.Add(handler.gameObject);
        }
        else if (handler.gameObject.tag == "Enemy")
        {
            enemyObjects.Add(handler.gameObject);
        }
    }


    public void Unsubscribe(TurnEventHandler handler)
    {
        turnHandlers.Remove(handler);

        if (handler.gameObject.tag == "Player")
        {
            playerObjects.Remove(handler.gameObject);
        }
        else if (handler.gameObject.tag == "Enemy")
        {
            enemyObjects.Remove(handler.gameObject);
        }
    }

    public void ChangeTurnState() // WILL BE CALLED BY ENDROUND BUTTON ON PLAYER TURN. AN AI MANAGER WILL CALL ON ENEMY TURN
    {
        if (turnState == TurnState.PlayerTurn)
        {
            turnState = TurnState.EnemyTurn;
            Debug.Log("ENEMY TURN");
        }
        else if (turnState == TurnState.EnemyTurn)
        {
            turnState = TurnState.PlayerTurn;
            Debug.Log("PlAYER TURN");
        }
        TurnStateChangeEvent();
    }
    private void TurnStateChangeEvent()
    {
        foreach (TurnEventHandler handler in turnHandlers)
        {
            handler.CallBackTurnEvent(turnState);
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    // Update is called once per frame
    void Update()
    {
        gameState = CheckWinCondition();
    }


    // THIS IS TEMPPORARY
    // TODO: Ideally checking win condition would be delegated to a scriptable object, which would allow customizable win condition
    private GameState CheckWinCondition()
    {
        if (enemyObjects.Count == 0)
        {
            return GameState.Won;
        }
        if (playerObjects.Count == 0)
        {
            return GameState.Lost;
        }

        return GameState.Ongoing;
    }
}
