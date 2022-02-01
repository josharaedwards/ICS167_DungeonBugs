using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPointDisplay : MonoBehaviour
{
    public Slider actionPointSlider;
    public Image fillImage;
    public GameObject endRoundButton;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI actionPointText;

    public string playerName;
    public int maxActionPoints;
    private int currentActionPoints;

    public Color fullActionColor;
    public Color emptyActionColor;

    private bool turnPassed;

    private void OnEnable()
    {
        playerNameText.text = playerName + "'s Mana";

        actionPointSlider.maxValue = maxActionPoints;

        currentActionPoints = maxActionPoints;

        turnPassed = false;

        UpdateAPUI();

        SetupEndRoundButton();
    }

    void Update()
    {
        if (currentActionPoints == maxActionPoints || turnPassed)
        {
            endRoundButton.SetActive(false);
        }
        else
        {
            endRoundButton.SetActive(true);
        }

        actionPointText.text = currentActionPoints.ToString();
    }

    public void UpdateAPPoints(int cost)
    {
        currentActionPoints -= cost;
        UpdateAPUI();
    }

    private void UpdateAPUI()
    {
        actionPointSlider.value = currentActionPoints;

        fillImage.color = Color.Lerp(emptyActionColor, fullActionColor, currentActionPoints / maxActionPoints);
    }

    private void SetupEndRoundButton()
    {
        Button endRound = endRoundButton.GetComponentInChildren<Button>();

        endRound.onClick.AddListener(() => EndPlayerRound());
    }

    private void EndPlayerRound()
    {
        GameManager.GetInstance().ChangeTurnState();
        turnPassed = true;
    }
}
