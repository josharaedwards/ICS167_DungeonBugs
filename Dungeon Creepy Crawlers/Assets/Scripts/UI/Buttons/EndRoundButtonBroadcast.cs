//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoundButtonBroadcast : DBButton
{
    public void Start()
    {
        Setup();
    }

    protected override void OnDBButtonClick()
    {
        base.OnDBButtonClick();
        GameManager.GetInstance().ChangeTurnState();
    }
}
