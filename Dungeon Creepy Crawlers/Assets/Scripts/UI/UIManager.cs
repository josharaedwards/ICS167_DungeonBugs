//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private GameObject unitInfoSpawnLoc;
    [SerializeField] private TextMeshProUGUI winLoseText;

    public static UIManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowWinLoseText(false);
        }
    }

    public Transform GetUnitSpawnLocation()
    {
        return unitInfoSpawnLoc.transform;
    }

    public void ShowWinLoseText(bool youWon)
    {
        if(youWon)
        {
            winLoseText.text = "You Won!";
        }
        else
        {
            winLoseText.text = "You Lose.";
        }

        winLoseText.gameObject.SetActive(true);
    }
}
