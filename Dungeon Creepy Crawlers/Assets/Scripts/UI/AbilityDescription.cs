using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityDescription : MonoBehaviour
{
    [SerializeField] private GameObject abilityTextUI;
    [SerializeField] private TextMeshProUGUI abilityText;

    public void Start()
    {
        AbilityButtonBroadcast.abilityHoverText += SetVisibility;
    }

    public void OnDisable()
    {
        AbilityButtonBroadcast.abilityHoverText -= SetVisibility;
    }

    private void SetVisibility(bool isHovering, Transform root, string description)
    {
        if (!hasSameRoot(root))
            return;

        if(isHovering)
        {
            abilityTextUI.SetActive(true);
            abilityText.text = description;
            return;
        }

        abilityTextUI.SetActive(false);
    }

    private bool hasSameRoot(Transform root)
    {
        return root == gameObject.transform.root;
    }
}
