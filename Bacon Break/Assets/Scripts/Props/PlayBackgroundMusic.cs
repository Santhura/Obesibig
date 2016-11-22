using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayBackgroundMusic : MonoBehaviour
{
    private GameManager gameManager;        //For getting the current scene name.
    private AudioSource audioSource;        //The audiosource attached to this gameobject.
    public AudioClip[] clipList;        //For storing the sounds for the menus and levels.

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.CurrentSceneName == "Main Menu" || gameManager.CurrentSceneName == "v2LevelSelect")
        {
            //Otherwise the clip would start all over again all the f*cking time.
            if (audioSource.clip != clipList[0])
            {
                audioSource.clip = clipList[0];
                PlaySound();
            }
        }

        if (gameManager.CurrentSceneName == "TutorialScene")
        {
            //Otherwise the clip would start all over again all the f*cking time.
            if (audioSource.clip != clipList[1])
            {
                audioSource.clip = clipList[1];
                PlaySound();
            }

            if (Application.isLoadingLevel)
            {
                RestartSound();
            }
        }
    }

    void PlaySound()
    {
        audioSource.Play();
    }

    void RestartSound()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
