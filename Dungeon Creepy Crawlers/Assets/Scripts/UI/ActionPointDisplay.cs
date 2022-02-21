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

    private bool toggle;

    private void Start()
    {
        playerManager = PlayerManager.GetInstance();
        playerNameText.text = playerName + "'s Mana";

        if (playerManager)
        {
            maxActionPoints = playerManager.GetMaxActionPoint();
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
        UpdateAPUI();
    }

    public void SetPlayerName(string playerName_)
    {
        playerName = playerName_;
    }

    private void UpdateAPUI()
    {
        if(playerManager)
        {
            currentActionPoints = playerManager.GetCurrentActionPoint();
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
