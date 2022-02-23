//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPointDisplay : MonoBehaviour
{
    private PlayerManager playerManager;
    private int maxActionPoints;
    private int currentActionPoints;

    [SerializeField] private string playerName;

    [SerializeField] private Slider actionPointSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI actionPointText;     

    [SerializeField] private Color fullActionColor;
    [SerializeField] private Color emptyActionColor;

    [SerializeField] private GameObject backgroundObj;
    [SerializeField] private GameObject fillAreaObj;
    [SerializeField] private GameObject apNumberObj;
    [SerializeField] private GameObject playerNameObj;
    [SerializeField] private GameObject endRoundButton;
    [SerializeField] private GameObject enemyTextObj;

    private GameManager.TurnState currentTurn = GameManager.TurnState.Player1Turn;

    private void Start()
    {
        playerManager = PlayerManager.GetInstance();
        SetPlayerName("Player 1");
    }

    void Update()
    {
        if(currentTurn != GameManager.TurnState.EnemyTurn)
            UpdateAPUI();
    }

    public void SetPlayerName(string playerName_)
    {
        playerName = playerName_;
        playerNameText.text = playerName + "'s Mana";
    }

    private void UpdateAPUI()
    {
        if(playerManager)
        {
            maxActionPoints = playerManager.GetMaxActionPoint(currentTurn);
            actionPointSlider.maxValue = maxActionPoints;

            currentActionPoints = playerManager.GetCurrentActionPoint(currentTurn);
            actionPointSlider.value = currentActionPoints;

            actionPointText.text = currentActionPoints.ToString();
            fillImage.color = Color.Lerp(emptyActionColor, fullActionColor, currentActionPoints / maxActionPoints);
        }
    }

    public void ChangeAPUIDisplay(GameManager.TurnState turnState)
    {
        switch (turnState)
        {
            case GameManager.TurnState.EnemyTurn:
                SetRoundDisplay(false);
                break;
            case GameManager.TurnState.Player1Turn:
                SetPlayerName("Player 1");
                SetRoundDisplay(true);
                break;
            case GameManager.TurnState.Player2Turn:
                SetPlayerName("Player 2");
                SetRoundDisplay(true);
                break;
            default:
                break;
        }

        currentTurn = turnState;
    }

    private void SetRoundDisplay(bool isPlayerTurn)
    {
        backgroundObj.SetActive(isPlayerTurn);
        fillAreaObj.SetActive(isPlayerTurn);
        apNumberObj.SetActive(isPlayerTurn);
        playerNameObj.SetActive(isPlayerTurn);
        endRoundButton.SetActive(isPlayerTurn);

        enemyTextObj.SetActive(!isPlayerTurn);
    }
}
