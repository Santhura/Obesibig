﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
    TODO:: Later on we have level select, instead of returning to the main menu, we have to return to the level select.

    possiblilty:
        1: or add a button in the win screen to go to the next level.
        2: For when you lose, maybe a button to retry the level.    
     
     */

public class WinOrLoseScript : MonoBehaviour {

    //  private CanvasGroup LoseAndWin_Panel;       // Display all the children.
    private bool canPlay = true;
    private Text winOrLose_Text;                // Display if you have won or losed
    private Button retunToMenu_Button;          // When in lose screen, button has to return to main menu.
    private GameObject panel_winLose;
    public HighscoreManager displayScore;
    public AudioClip[] winOrLoseAudio;
    private AudioSource SelectedAudio;

    public static bool hasWon;                  // check if the player has won.
    public static bool isDead;                  // check if the player is dead.

    void Awake() {
        hasWon = false;
        // LoseAndWin_Panel = GameObject.Find("LoseAndWin_Panel").GetComponent<CanvasGroup>();
        panel_winLose = GameObject.Find("LoseAndWin_Panel");
        displayScore = GameObject.Find("Score Manager").GetComponent<HighscoreManager>();
        winOrLose_Text = GameObject.Find("Text_WinOrLose").GetComponent<Text>();
        retunToMenu_Button = GameObject.Find("Button_ReturnToLevelSelect").GetComponent<Button>();
        //    LoseAndWin_Panel.alpha = 0;
        retunToMenu_Button.interactable = false;
        panel_winLose.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        SelectedAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (hasWon) // Display winning screen
        {
            if (canPlay) {
                Time.timeScale = 0;
                SelectedAudio.clip = winOrLoseAudio[0];
                SelectedAudio.Play();
                canPlay = false;
            }
            else
                DisableAll();

            panel_winLose.SetActive(true);
            displayScore.TriggerScore();
            //  LoseAndWin_Panel.alpha = 1;
            winOrLose_Text.text = "Level Completed!";
            retunToMenu_Button.interactable = true;

            //Unlock next level
            PlayerPrefs.SetInt("Unlock", 1);

            PlayerMovement.isAbleToMove = false;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMoveTemp = false;
            // Time.timeScale = 0;
        }
        else if (isDead) // display losing screen
        {
            StartCoroutine(WaitForDeathScreen(2));
        }
    }

    /// <summary>
    /// Stop displaying the lose screen, and buttons can't interact
    /// </summary>
    private void DisableAll() {

        // LoseAndWin_Panel.alpha = 0;
        // retunToMenu_Button.interactable = false;
        hasWon = false;
        isDead = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Function to return to main menu.
    /// </summary>
    public void ReturnToMenu() {
        DisableAll();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Wait for couple seconds to show death screen
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator WaitForDeathScreen(float time) {
        yield return new WaitForSeconds(time);
        panel_winLose.SetActive(true);
        winOrLose_Text.text = "The pig is slaughtered";
        PlayerPrefs.SetInt("Unlock", 0);
        retunToMenu_Button.interactable = true;

        if (canPlay) {
            SelectedAudio.clip = winOrLoseAudio[1];
            SelectedAudio.Play();
            Time.timeScale = 0;
            canPlay = false;
        }
        else
            DisableAll();
    }
}
