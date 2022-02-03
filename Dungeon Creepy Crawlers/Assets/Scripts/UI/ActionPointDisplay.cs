//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPointDisplay : MonoBehaviour
{
    public PlayerManager playerManager;

    public Slider actionPointSlider;
    public Image fillImage; 
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI actionPointText;

    public string playerName;
    public int maxActionPoints;
    private int currentActionPoints;

    public Color fullActionColor;
    public Color emptyActionColor;

    public GameObject backgroundObj;
    public GameObject fillAreaObj;
    public GameObject apNumberObj;
    public GameObject playerNameObj;
    public GameObject endRoundButton;
    public GameObject enemyTextObj;

    private bool toggle;



    private void OnEnable()
    {
        playerManager = PlayerManager.GetInstance();
        playerNameText.text = playerName + "'s Mana";

        if(playerManager)
        {
            maxActionPoints = playerManager.GetActionPoint();
            actionPointSlider.maxValue = maxActionPoints;
        }
        else
        {
            Debug.Log("ERROR: Missing Player Manager on AP UI");
        }

        toggle = false;

        SetupEndRoundButton();
    }

    void Update()
    {
        if (currentActionPoints == maxActionPoints || toggle)
        {
            endRoundButton.SetActive(false);
        }
        else
        {
            endRoundButton.SetActive(true);
        }

        UpdateAPUI();
    }

    public void UpdateAPPoints(int cost)
    {
        //currentActionPoints -= cost;
        //UpdateAPUI();
    }

    private void UpdateAPUI()
    {
        if(playerManager)
        {
            currentActionPoints = playerManager.GetActionPoint();
            actionPointSlider.value = currentActionPoints;

            actionPointText.text = currentActionPoints.ToString();
            fillImage.color = Color.Lerp(emptyActionColor, fullActionColor, currentActionPoints / maxActionPoints);
        }
    }

    private void SetupEndRoundButton()
    {
        Button endRound = endRoundButton.GetComponentInChildren<Button>();

        endRound.onClick.AddListener(() => EndPlayerRound());
    }

    private void EndPlayerRound()
    {
        GameManager.GetInstance().ChangeTurnState();
    }

    public void ToggleRoundDisplay()
    {
        if(toggle)
        {
            toggle = false;
        }
        else
        {
            toggle = true;
        }

        backgroundObj.SetActive(!toggle);
        fillAreaObj.SetActive(!toggle);
        apNumberObj.SetActive(!toggle);
        playerNameObj.SetActive(!toggle);
        endRoundButton.SetActive(!toggle);

        enemyTextObj.SetActive(toggle);
    }
}
