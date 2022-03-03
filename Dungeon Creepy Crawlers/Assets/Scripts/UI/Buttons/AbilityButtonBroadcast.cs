//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonBroadcast : DBButton
{
    private Ability ability;
    private AbilityHandler abilityHandler;

    public void OnEnable()
    {
        Setup();
    }

    public void Init(Ability ability_, AbilityHandler abilityHandler_)
    {
        ability = ability_;
        abilityHandler = abilityHandler_;

        string abilityDisplayText = ability.abilityName + " - " + ability.apCost;

        self.GetComponentInChildren<TextMeshProUGUI>().text = abilityDisplayText;
    }

    protected override void OnDBButtonClick()
    {
        base.OnDBButtonClick();
        BroadcastAbility();
    }

    private void BroadcastAbility()
    {
        if (abilityHandler)
        {
            Debug.Log("Broadcasting Ability: " + ability.abilityName);
            abilityHandler.Select(ability);
        }
        else
        {
            Debug.Log("Stats Tracker missing");
        }
    }

    public void OnHoverEnter()
    {
        abilityHandler.SelectHover(ability);
    }

    public void OnHoverExit()
    {
        abilityHandler.DeselectHover();
    }
}
