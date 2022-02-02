//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => LoadGame());
        quitButton.onClick.AddListener(() => QuitGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
