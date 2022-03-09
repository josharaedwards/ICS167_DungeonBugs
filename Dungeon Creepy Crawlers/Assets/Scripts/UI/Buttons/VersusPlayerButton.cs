using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusPlayerButton : DBButton
{
    void Start()
    {
        Setup();
    }

    protected override void OnDBButtonClick()
    {
        base.OnDBButtonClick();
        SceneManager.LoadScene("PVP");
    }
}
