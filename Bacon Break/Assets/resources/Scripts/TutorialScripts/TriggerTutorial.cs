using UnityEngine;
using System.Collections;

public class TriggerTutorial : MonoBehaviour {
    public GameObject tutorialObject; //the finger cursor
    public float timeLeft = .4f; //the amount of time the tutorial lasts, 
    //^NOTE: time scale will be 0.1 so this has been divided by 10 to equate to seconds. (0.1 = 1 second)
    bool startCounting = false; //this allows the countdown

    void Update()
    {
        
        if (startCounting) //count down when this is true
            timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) //once the time is up, reset the time scale, remove the finger cursor and stop counting down.
        {
            Time.timeScale = 1.0f;
            tutorialObject.SetActive(false);
            startCounting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //colliding with the player will start the tutorial
        {
            Time.timeScale = 0.1f; //slow down time to make the player aware of the tutorial
            tutorialObject.SetActive(true); //set the finger cursor active to start the tutorial
            startCounting = true; //allow the countdown to happen in update.
        }
    }

}
