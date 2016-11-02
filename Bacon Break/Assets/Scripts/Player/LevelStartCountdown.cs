using UnityEngine;
using System.Collections;

public class LevelStartCountdown : MonoBehaviour {

    float countDown = 3;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        Camera.main.orthographicSize = 75;
        PlayerMovement.isAbleToMove = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;
            Camera.main.orthographicSize -= Time.deltaTime * 10;
        }
        else
        {
            PlayerMovement.isAbleToMove = true;
            Camera.main.orthographicSize = 35;
            Destroy(player.GetComponent<LevelStartCountdown>());
        }

        Debug.Log((int)countDown);
    }
}
