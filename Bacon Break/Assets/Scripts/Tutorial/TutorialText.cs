using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {
    private Text tutorialText;
    float timeLeft; //the amount of time the tutorial lasts, 
    public float eventTime = 5.0f; //the initial time of the tutorial, you can use this to reset the timer.
    public string myText;
    //^NOTE: time scale will be 0.1 so this has been divided by 10 to equate to seconds. (0.1 = 1 second)
    bool startCounting = false; //this allows the countdown

    void Start () {
        timeLeft = eventTime;
        tutorialText = GameObject.Find("TutorialText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounting) //count down when this is true
            timeLeft -= Time.deltaTime;
        if (!startCounting)
            timeLeft = eventTime;

        if (timeLeft <= 0) //once the time is up, reset the time scale, remove the finger cursor and stop counting down.
        {
            startCounting = false;
            tutorialText.text = "";
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //colliding with the player will start the tutorial
        {
            timeLeft = eventTime;
            startCounting = true;
            tutorialText.text = myText;

        }
    }

}
