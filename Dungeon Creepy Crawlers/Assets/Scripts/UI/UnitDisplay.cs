using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color greyedColor;
    private Color defaultColor;

    public void OnEnabled()
    {
        defaultColor = sprite.color;
    }

    public void UpdatePlayerIcon(bool hasUsedAbility)
    {
        if (hasUsedAbility)
        {
            sprite.color = greyedColor;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }
}
