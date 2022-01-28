using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEventHandler : MonoBehaviour, TurnEventReciever
{
    public GameManager.TurnState turn; // TurnState the object moves in

    private List<TurnEventReciever> turnEventRecievers; // List of component need to receive turn state switch event

    private GameManager gameManager;



    void Awake()
    {
        turnEventRecievers = new List<TurnEventReciever>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        gameManager.Subscribe(this);
    }

    public void Subscribe(TurnEventReciever reciever)
    {
        turnEventRecievers.Add(reciever);
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        foreach (TurnEventReciever reciever in turnEventRecievers)
        {
            reciever.CallBackTurnEvent(turnState);
        }
    }
}
