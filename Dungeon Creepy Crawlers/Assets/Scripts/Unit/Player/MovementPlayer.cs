// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementPlayer : Movement, TurnEventReciever
{
    private TurnEventHandler turnEventHandler;

    public delegate void OnUpdateMovemntUI(bool isMovable, Transform root);
    public static event OnUpdateMovemntUI updateMovementUI;

    protected override void Start()
    {
        base.Start();

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

        if (turnEventHandler.turn == GameManager.TurnState.Player1Turn) // By default the player start first, will change this for multiplayer
            movable = true;
        else
            movable = false;
    }

    public override SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        if (!validMoveCellPos.Contains(cellPos))
        {
            return null; // Deselect happens
        }

        bool t = Move(cellPos);
        if (t)
        {
            return selectionHandler;
        }

        return null;
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        if (turnState != turnEventHandler.turn)
        {
            DisableMovement();
        }
        else if (turnState == turnEventHandler.turn)
        {
            EnableMovement();
        }
    }

    public override void DisableMovement()
    {
        base.DisableMovement();
        updateMovementUI(movable, gameObject.transform);
    }

    public override void EnableMovement()
    {
        base.EnableMovement();
        updateMovementUI(movable, gameObject.transform);
    }
}

