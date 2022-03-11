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
    public Transform damagePopupInst;

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

    IEnumerator WaitForWinLose(GameManager.GameState currentState)
    {
        yield return new WaitForSeconds(3);

        GoToNextLevel(currentState);
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
            case GameManager.GameState.Lost:
                SetEndGameText(currentState);
                winLoseText.gameObject.SetActive(true);
                StartCoroutine(WaitForWinLose(currentState));
                break;
            case GameManager.GameState.Ongoing:
            default:
                break;
        }
    }

    private void SetEndGameText(GameManager.GameState currentstate)
    {
        if(currentstate == GameManager.GameState.Lost)
        {
            winLoseText.text = "You Lose. \n Try Again!";
        }
        else
        {
            winLoseText.text = "You Won! \n";
        }

        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.buildIndex < 3)
        {
            winLoseText.text += "LEVEL UP!";
        }
    }

    private void GoToNextLevel(GameManager.GameState currentstate)
    {
        if(currentstate == GameManager.GameState.Lost)
        {
            SceneManager.LoadScene(0);
            return;
        }

        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.buildIndex >= 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            int nextScene = currentScene.buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }
    }
}
