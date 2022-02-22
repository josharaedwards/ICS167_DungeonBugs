using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBButton : MonoBehaviour
{
    protected Button self;
    private AudioManager audioManager;

    [SerializeField] private AudioClip buttonSFX;

    protected void Setup()
    {
        self = GetComponent<Button>();
        self.onClick.AddListener(() => OnDBButtonClick());
        audioManager = AudioManager.GetInstance();
    }

    protected virtual void OnDBButtonClick()
    {
        audioManager.PlayButtonSFX(buttonSFX);
    }
}
