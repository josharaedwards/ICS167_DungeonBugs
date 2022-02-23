using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandlerPlayer : AbilityHandler, TurnEventReciever
{
    private TurnEventHandler turnEventHandler;
    private PlayerManager playerManager;

    protected override void Start()
    {
        base.Start();

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

        playerManager = PlayerManager.GetInstance();

        if (turnEventHandler.turn == GameManager.TurnState.Player2Turn)
        {
            DisableCast();
        }

    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        if (turnState != turnEventHandler.turn)
        {
            DisableCast();
        }
        else if (turnState == turnEventHandler.turn)
        {
            EnableCast();
        }
    }

    public override SelectionHandler CallBackSelect(Vector3Int targetPos)
    {
        if (selectedAbility == null || !castable)
        {
            return null;
        }

        GameObject target = gridManager.GetObjectFromCell(targetPos);
        if (target == null)
        {
            return null;
        }

        StatsTracker targetStatsTracker = target.GetComponent<StatsTracker>();
        if (targetStatsTracker == null)
        {
            return null;
        }
        bool result = playerManager.Cast(GetComponent<StatsTracker>(), targetStatsTracker, selectedAbility);
        if (result)
        {
            DisableCast();
        }
        return null;

    }
}
