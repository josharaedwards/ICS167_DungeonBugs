using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInfoDisplay : MonoBehaviour
{
    public string unitName;
    public Sprite unitImage;

    public Image unitImageObject;
    public TextMeshProUGUI unitNameText;

    public GameObject buttonContainer;
    public Button abilityButtonPrototype;

    private string[] testAbilityNames = { "Melee Attack", "Healing Slime", "Snail Trail", "Song of Vitality" };

    private void OnEnable()
    {
        unitNameText.text = unitName;
        unitImageObject.sprite = unitImage;

        //Debug for testing out updating abilities
        UpdateAbilites(testAbilityNames);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateAbilites(string[] abilitiesIn)
    {
        int numOfAbilities = abilitiesIn.Length;

        foreach(Transform child in buttonContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < numOfAbilities; ++i)
        {
            CreateAbilityButton(abilitiesIn[i]);
        }
    }

    private void CreateAbilityButton(string abilityText)
    {
        Button abilityButton = Instantiate(abilityButtonPrototype, buttonContainer.transform);
        abilityButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityText;
    }
}
