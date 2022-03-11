//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementUI : MonoBehaviour
{
    [SerializeField] private Image movementUI;

    public void OnEnable()
    {
        MovementPlayer.updateMovementUI += UpdateDisplay;
    }

    public void OnDisable()
    {
        MovementPlayer.updateMovementUI -= UpdateDisplay;
    }

    private void UpdateDisplay(bool isMovable, Transform root)
    {
        if (!hasSameRoot(root))
            return;

        movementUI.enabled = isMovable;
    }

    private bool hasSameRoot(Transform root)
    {
        return root == gameObject.transform.root;
    }
}
