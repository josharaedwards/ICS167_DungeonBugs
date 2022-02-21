//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioSource buttonSFXSource;
    private AudioSource backgroundMusic;


    public static AudioManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(transform.gameObject);
        backgroundMusic = GetComponent<AudioSource>();
    }

    public void PlayButtonSFX(AudioClip buttonSFX)
    {
        if(buttonSFX)
        {
            buttonSFXSource.clip = buttonSFX;
            buttonSFXSource.Play();
        }
    }
}
