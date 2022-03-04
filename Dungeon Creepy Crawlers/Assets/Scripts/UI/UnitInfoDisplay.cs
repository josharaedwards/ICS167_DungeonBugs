//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInfoDisplay : MonoBehaviour
{
    [SerializeField] private Image unitImageObject;
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthBarValue;

    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private Button abilityButtonPrototype;

    private StatsTracker unitStats;


    public void Init(StatsTracker unitStats_)
    {
        unitStats = unitStats_;

        unitNameText.text = unitStats.unit.type;
        unitImageObject.sprite = unitStats.unit.fullSprite;

        stats stats_ = unitStats.GetStats();
        healthBar.maxValue = stats_.maxHP;
        healthBar.value = stats_.hp;

        healthBarValue.text = stats_.hp.ToString() + " / " + stats_.maxHP.ToString();

        UpdateAbilities();
    }

    public void UpdateAbilities()
    {
        if (!unitStats)
            return;
            
        AbilityHandler abilityHandler = unitStats.GetAbilityHandler();
        List<Ability> abilities = abilityHandler.GetAbilities();

        int numOfAbilities = abilities.Count;

        foreach (Transform child in buttonContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < numOfAbilities; ++i)
        {
            CreateAbilityButton(abilities[i], abilityHandler);
        }
    }

    private void CreateAbilityButton(Ability ability, AbilityHandler abilityHandler)
    {
        Button abilityButton = Instantiate(abilityButtonPrototype, buttonContainer.transform);
        abilityButton.GetComponent<AbilityButtonBroadcast>().Init(ability, abilityHandler);
    }
}
