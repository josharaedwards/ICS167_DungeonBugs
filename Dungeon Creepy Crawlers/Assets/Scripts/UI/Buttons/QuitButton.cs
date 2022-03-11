//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : DBButton
{
    void Start()
    {
        Setup();
    }

    protected override void OnDBButtonClick()
    {
        base.OnDBButtonClick();
        Application.Quit();
    }
}
