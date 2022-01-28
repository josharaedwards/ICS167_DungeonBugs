using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface TurnEventReciever
{
    public void CallBackTurnEvent(GameManager.TurnState turnState);

}
