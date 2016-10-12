using UnityEngine;
using System.Collections;

public class TriggerTutorial : MonoBehaviour {
    public GameObject tutorialObject; //the finger cursor
    public float timeLeft = .4f; //the amount of time the tutorial lasts, 
    public float eventTime; //the initial time of the tutorial, you can use this to reset the timer.
    public bool slow; //decide whether the game should slow down when colliding with the tutorial trigger.
    //^NOTE: time scale will be 0.1 so this has been divided by 10 to equate to seconds. (0.1 = 1 second)
    bool startCounting = false; //this allows the countdown

    void Update()
    {
        
        if (startCounting) //count down when this is true
            timeLeft -= Time.deltaTime;
        if (!startCounting)
            timeLeft = eventTime;

        if (timeLeft <= 0) //once the time is up, reset the time scale, remove the finger cursor and stop counting down.
        {
            if (slow)
            Time.timeScale = 1.0f;

            if (tutorialObject)
            tutorialObject.SetActive(false);

            startCounting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //colliding with the player will start the tutorial
        {
            if (slow)
            Time.timeScale = 0.2f; //slow down time to make the player aware of the tutorial

            if (tutorialObject)
            tutorialObject.SetActive(true); //set the finger cursor active to start the tutorial

            startCounting = true; //allow the countdown to happen in update.
        }
    }

}
