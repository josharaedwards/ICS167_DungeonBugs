using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInfoDisplay : MonoBehaviour
{
    public Image unitImageObject;
    public TextMeshProUGUI unitNameText;
    public Slider healthBar;
    public TextMeshProUGUI healthBarValue;

    public GameObject buttonContainer;
    public Button abilityButtonPrototype;

    public StatsTracker unitStats;

    private void OnEnable()
    {
        Init();
        UpdateAbilities();
    }

    public void Init()
    {
        if(unitStats)
        {
            unitNameText.text = unitStats.unit.type;
            unitImageObject.sprite = unitStats.unit.fullSprite;

            healthBar.maxValue = unitStats.maxHP;
            healthBar.value = unitStats.hp;

            healthBarValue.text = unitStats.hp.ToString() + " / " + unitStats.maxHP.ToString();
        }
        else
        {
            unitNameText.text = "Debug";
        }
    }

    public void UpdateAbilities()
    {
        if (!unitStats)
            return;

        int numOfAbilities = unitStats.abilities.Length;

        foreach (Transform child in buttonContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < numOfAbilities; ++i)
        {
            CreateAbilityButton(unitStats.abilities[i]);
        }
    }

    private void CreateAbilityButton(Ability ability)
    {
        Button abilityButton = Instantiate(abilityButtonPrototype, buttonContainer.transform);
        abilityButton.GetComponent<AbilityButtonBroadcast>().Init(ability);
    }
}
