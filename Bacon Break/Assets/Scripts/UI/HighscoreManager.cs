using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using GooglePlayGames;

public class HighscoreManager : MonoBehaviour {
    public float timeLeft;                  //the amount of timeleft is a way to calculate highscore.
    private float maxTime;                  //used to display the amount of time the player used to complete the level
    private Text text_highScore;            //When you win, the game will display your highscore.
    public ScoreScript baconsCollected;      // this gameobject will call upon the score script to count the amount of bacons collected.
    public float highScore;                 //the actual highscore.
    public int trapsDestroyedAmount;
    private bool scoreTriggered;


    void Awake()
    {
        scoreTriggered = false;
        baconsCollected = GameObject.Find("pnl_score").GetComponent<ScoreScript>();
        maxTime = timeLeft;
        trapsDestroyedAmount = 0;                               
        text_highScore = GameObject.Find("Text_HighScore").GetComponent<Text>();    //find the highscore panel
        text_highScore.text = " ";                                                  // initially, the score shouldn't display anything
     
    }
        // Update is called once per frame
    void Update ()
    {
        if (timeLeft > 0 && !scoreTriggered)
        {
            timeLeft -= Time.deltaTime;                     //countdown the time
            highScore = Mathf.FloorToInt(timeLeft) * 100 + (baconsCollected.baconAmount * 200) + (trapsDestroyedAmount * 200);   //make the score equate to the time, but convert it to int.
        }
       else
            timeLeft = 0;                                   //negative score makes no sense, so cap it at 0;
    }

    public void TriggerScore()
    {
        scoreTriggered = true;
        maxTime = maxTime - timeLeft;
        text_highScore.text = "Score: " + highScore + "\nTime: " + System.Math.Round(maxTime, 2) + " seconds\nBacons Collected: " + baconsCollected.baconAmount + "\nTraps Destroyed " + trapsDestroyedAmount;

        //Update the leaderboard with new score
        // Submit leaderboard scores, if authenticated
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            long score = (long)highScore;
            // Note: make sure to add 'using GooglePlayGames'
            PlayGamesPlatform.Instance.ReportScore(score,
                GPGSIds.leaderboard_world_highscore,
                (bool success) =>
                {
                    Debug.Log("(BaconBreak) Leaderboard update success: " + success);
                });
        }

        PlayerPrefs.SetInt("Level " + PlayerPrefs.GetInt("LevelIndex") + "_score", Mathf.FloorToInt(highScore));
    }
}
