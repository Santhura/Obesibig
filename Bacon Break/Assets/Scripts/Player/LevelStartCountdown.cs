using UnityEngine;
using System.Collections;

public class LevelStartCountdown : MonoBehaviour {

    public float countDown = 3; // countdown in seconds
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        Camera.main.orthographicSize = 75;

        // this stops all control over the player
        PlayerMovement.isCountdown = true;
    }
	
	// Update is called once per frame
	void Update () {
        // saving the remaining time from the previous update loop
        var temp = countDown;

        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;

            // calculate the amounnt of distance the camera needs to move in the time that has passed in the current update loop
            var move = (Camera.main.orthographicSize - 35) / (temp / Time.deltaTime);

            // resize the camera, with an orthographic camera this gives the illusion of a camera closing in on the player
            Camera.main.orthographicSize -= move;
        }
        else
        {
            // give back control to the player
            PlayerMovement.isCountdown = false;

            // make sure the camera has the correct final size
            Camera.main.orthographicSize = 35;

            // destroy this script as its not required anymore after countdown
            Destroy(player.GetComponent<LevelStartCountdown>());
        }

        Debug.Log((int)countDown);
    }
}
