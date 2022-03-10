using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusAIButton : DBButton
{
    void Start()
    {
        Setup();
    }

    protected override void OnDBButtonClick()
    {
        base.OnDBButtonClick();
        SceneManager.LoadScene("PvAI");
    }
}
