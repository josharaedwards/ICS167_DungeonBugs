//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private Color greyedColor;
    private Color defaultColor;

    public void Awake()
    {
        defaultColor = sprite.color;
        greyedColor = defaultColor;
        greyedColor.g -= 0.45f;

        AbilityHandlerPlayer.updateUnitDisplay += UpdatePlayerIcon;
    }

    public void OnDisable()
    {
        AbilityHandlerPlayer.updateUnitDisplay -= UpdatePlayerIcon;
    }

    public void UpdatePlayerIcon(bool hasUsedAbility, Transform root)
    {
        if (!hasSameRoot(root))
            return;

        if (hasUsedAbility)
        {
            sprite.color = greyedColor;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }

    private bool hasSameRoot(Transform root)
    {
        return root == gameObject.transform.root;
    }
}
