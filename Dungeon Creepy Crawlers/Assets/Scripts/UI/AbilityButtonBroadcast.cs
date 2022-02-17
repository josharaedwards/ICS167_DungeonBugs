//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonBroadcast : MonoBehaviour
{
    public Ability ability;
    public StatsTracker unit;

    private Button self;

    public void OnEnable()
    {
        self = GetComponent<Button>();
    }

    public void Init(Ability abilityIn, StatsTracker unitIn)
    {
        ability = abilityIn;
        unit = unitIn;

        self.GetComponentInChildren<TextMeshProUGUI>().text = ability.abilityName;
        self.onClick.AddListener(() => BroadcastAbility());
    }

    private void BroadcastAbility()
    {
        if(unit)
        {
            Debug.Log("Broadcasting Ability: " + ability.abilityName);
            unit.SelectAbility(ability);
        }
        else
        {
            Debug.Log("Stats Tracker missing");
        }
    }

    public void OnHoverEnter()
    {
        unit.SelectAbility(ability);
    }

    public void OnHoverExit()
    {
        unit.DeselectAbility();
    }
}
