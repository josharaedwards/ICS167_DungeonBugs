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
        Player1Turn,
        Player2Turn,
        EnemyTurn
    }

    public enum GameMode
    {
        PVE,
        PVP
    }

    [SerializeField]  private GameMode gameMode = GameMode.PVE;
    [SerializeField]  private GameState gameState;
    [SerializeField]  private TurnState turnState;

    private HashSet<TurnEventHandler> turnHandlers;
    private HashSet<TurnEventHandler> toBeRemovedTurnHandlers;

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
        toBeRemovedTurnHandlers = new HashSet<TurnEventHandler>();

        playerObjects = new HashSet<GameObject>();
        enemyObjects = new HashSet<GameObject>();
    }

    void Start()
    {
        turnState = TurnState.Player1Turn;
        //Debug.Log("PlAYER TURN");
    }

    public void Subscribe(TurnEventHandler handler)
    {
        turnHandlers.Add(handler);

        if (handler.turn == TurnState.Player1Turn)
        {
            playerObjects.Add(handler.gameObject);
        }
        else if (handler.turn == TurnState.EnemyTurn)
        {
            enemyObjects.Add(handler.gameObject);
        }
    }


    public void Unsubscribe(TurnEventHandler handler)
    {
        toBeRemovedTurnHandlers.Add(handler); // Add to this set to be removed after the loop in TurnStateChangeEvent is done

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
        if (gameMode == GameMode.PVE)
        {
            ChangeTurnStatePVE();
        }
        else if (gameMode == GameMode.PVE)
        {
            ChangeTurnStatePVP();
        }
        TurnStateChangeEvent();
    }
    private void TurnStateChangeEvent()
    {
        toBeRemovedTurnHandlers.Clear();

        foreach (TurnEventHandler handler in turnHandlers)
        {
            handler.CallBackTurnEvent(turnState);
        }

        foreach (TurnEventHandler handler in toBeRemovedTurnHandlers) // Remove any object in this set
        {
            turnHandlers.Remove(handler);
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

    private void ChangeTurnStatePVE()
    {
        if (turnState == TurnState.Player1Turn)
        {
            turnState = TurnState.EnemyTurn;
            //Debug.Log("ENEMY TURN");
        }
        else if (turnState == TurnState.EnemyTurn)
        {
            turnState = TurnState.Player1Turn;
            //Debug.Log("PlAYER TURN");
        }
    }

    private void ChangeTurnStatePVP()
    {
        if (turnState == TurnState.Player1Turn)
        {
            turnState = TurnState.Player2Turn;
        }
        else if (turnState == TurnState.Player2Turn)
        {
            turnState = TurnState.Player1Turn;
        }
    }
}
