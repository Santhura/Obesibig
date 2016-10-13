using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {
    public float timeLeft;                  //the amount of timeleft is a way to calculate highscore.
    private float maxTime;                  //used to display the amount of time the player used to complete the level
    private Text text_highScore;            //When you win, the game will display your highscore.
    public ScoreScript baconsCollected;      // this gameobject will call upon the score script to count the amount of bacons collected.
    public float highScore;                 //the actual highscore.
    public int trapsDestroyedAmount;


    void Awake()
    {
        baconsCollected = GameObject.Find("pnl_score").GetComponent<ScoreScript>();
        maxTime = timeLeft;
        trapsDestroyedAmount = 0;                               
        text_highScore = GameObject.Find("Text_HighScore").GetComponent<Text>();    //find the highscore panel
        text_highScore.text = " ";                                                  // initially, the score shouldn't display anything
     
    }
        // Update is called once per frame
    void Update ()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;                     //countdown the time
            highScore = Mathf.FloorToInt(timeLeft) * 100 + (baconsCollected.baconAmount * 200) + (trapsDestroyedAmount * 200);   //make the score equate to the time, but convert it to int.
        }
       else
            timeLeft = 0;                                   //negative score makes no sense, so cap it at 0;
    }

    public void TriggerScore()
    {
        maxTime = maxTime - timeLeft;
        text_highScore.text = "Score: " + highScore + "\nTime: " + System.Math.Round(maxTime, 2) + " seconds\nBacons Collected: " + baconsCollected.baconAmount + "\nTraps Destroyed" + trapsDestroyedAmount;
    }
}
