using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStartCountdown : MonoBehaviour
{
    public float countDown = 4; // countdown in seconds
    public Text count; // text drawn on the screen
    GameObject player;

    // Use this for initialization
    void Start()
    {
        // fading background color of the countdown timer
        gameObject.GetComponent<Image>().CrossFadeAlpha(0.1f, countDown-1, false);

        // set the size of the orthographic camera to simulate a zoomed out camera
        Camera.main.orthographicSize = 75;

        // this stops all control over the player
        // temporarily solving this using a second variable
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().isAbleToMoveTemp = false;
        //PlayerMovement.isAbleToMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        // saving the remaining time from the previous update loop
        var temp = countDown;

        // countdown in seconds
        if (countDown >= 0)
        {
            // I had to first cast the float to an int and then to a string in order to only draw single digits on the screen
            var toInt = (int)countDown;
            count.text = toInt.ToString();

            countDown -= Time.deltaTime;

            // on the last second text becomes GO
            if (countDown <= 1)
                count.text = "GO";

            // calculate the amounnt of distance the camera needs to move in the time that has passed in the current update loop
            var move = (Camera.main.orthographicSize - 35) / (temp / Time.deltaTime);

            // resize the camera, with an orthographic camera this gives the illusion of a camera closing in on the player
            Camera.main.orthographicSize -= move;
        }
        else
        {
            // give back control to the player
            player.GetComponent<PlayerMovement>().isAbleToMoveTemp = true;
            //PlayerMovement.isAbleToMove = true;

            // make sure the camera has the correct final size
            Camera.main.orthographicSize = 35;

            // destroy this script as its not required anymore after countdown
            Destroy(gameObject);
        }
    }
}
