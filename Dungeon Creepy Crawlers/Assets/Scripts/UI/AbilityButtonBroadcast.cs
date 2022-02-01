using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonBroadcast : MonoBehaviour
{
    public Ability ability;

    private Button self;

    public void OnEnable()
    {
        self = GetComponent<Button>();
    }

    public void Init(Ability abilityIn)
    {
        ability = abilityIn;

        self.GetComponentInChildren<TextMeshProUGUI>().text = ability.abilityName;
        self.onClick.AddListener(() => BroadcastAbility());
    }

    private void BroadcastAbility()
    {
        //Insert code to prepare executing the ability
        Debug.Log("Broadcasting Ability: " + ability.abilityName);
    }
}
