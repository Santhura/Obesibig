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

    private CanvasGroup LoseAndWin_Panel;       // Display all the children.
    private Text winOrLose_Text;                // Display if you have won or losed
    private Button retunToMenu_Button;          // When in lose screen, button has to return to main menu.

    public static bool hasWon;                  // check if the player has won.
    public static bool isDead;                  // check if the player is dead.

    void Awake()
    {
        hasWon = false;
        LoseAndWin_Panel = GameObject.Find("LoseAndWin_Panel").GetComponent<CanvasGroup>();
        winOrLose_Text = GameObject.Find("Text_WinOrLose").GetComponent<Text>();
        retunToMenu_Button = GameObject.Find("Button_ReturnToMenu").GetComponent<Button>();
        LoseAndWin_Panel.alpha = 0;
        retunToMenu_Button.interactable = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(hasWon) // Display winning screen
        {
            LoseAndWin_Panel.alpha = 1;
            winOrLose_Text.text = "You have completed the level!!";
            retunToMenu_Button.interactable = true;
        }
        else if(isDead) // display losing screen
        {
            LoseAndWin_Panel.alpha = 1;
            winOrLose_Text.text = "The pig is slaughtered";
            retunToMenu_Button.interactable = true;
        }
	}
    
    /// <summary>
    /// Stop displaying the lose screen, and buttons can't interact
    /// </summary>
    private void DisableAll()
    {
        LoseAndWin_Panel.alpha = 0;
        retunToMenu_Button.interactable = false;
        hasWon = false;
        isDead = false;
    }

    /// <summary>
    /// Function to return to main menu.
    /// </summary>
    public void ReturnToMenu()
    {
        DisableAll();
        SceneManager.LoadScene(0);
    }
}
