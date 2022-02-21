//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonBroadcast : MonoBehaviour
{
    private Ability ability;
    private AbilityHandler abilityHandler;
    private bool isClicked;

    private Button self;

    public void OnEnable()
    {
        self = GetComponent<Button>();
    }

    public void Init(Ability ability_, AbilityHandler abilityHandler_)
    {
        ability = ability_;
        abilityHandler = abilityHandler_;

        self.GetComponentInChildren<TextMeshProUGUI>().text = ability.abilityName;
        self.onClick.AddListener(() => BroadcastAbility());

        isClicked = false;
    }

    private void BroadcastAbility()
    {
        isClicked = true;

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
        abilityHandler.Select(ability);
    }

    public void OnHoverExit()
    {
       if(!isClicked)
            abilityHandler.Deselect();
    }
}
