//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour, TurnEventReciever
{
    private static UIManager instance;

    [SerializeField] private GameObject unitInfoSpawnLoc;
    [SerializeField] private TextMeshProUGUI winLoseText;

    private ActionPointDisplay apBarUI;
    private TurnEventHandler turnEventHandler;

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

    void Start()
    {
        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

        apBarUI = GetComponentInChildren<ActionPointDisplay>();
    }

    void Update()
    {
        ShowWinLoseText(GameManager.GetInstance().GetGameState());
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        apBarUI.ChangeAPUIDisplay(turnState);
    }

    IEnumerator WaitForWinLose()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMenu");
    }

    public Transform GetUnitSpawnLocation()
    {
        return unitInfoSpawnLoc.transform;
    }

    public void ShowWinLoseText(GameManager.GameState currentState)
    {
        switch (currentState)
        {
            case GameManager.GameState.Won:
                winLoseText.text = "You Won!";
                winLoseText.gameObject.SetActive(true);
                StartCoroutine(WaitForWinLose());
                break;
            case GameManager.GameState.Lost:
                winLoseText.text = "You Lose.";
                winLoseText.gameObject.SetActive(true);
                StartCoroutine(WaitForWinLose());
                break;
            case GameManager.GameState.Ongoing:
            default:
                break;
        }
    }
}
