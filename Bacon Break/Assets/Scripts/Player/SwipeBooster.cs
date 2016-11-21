using UnityEngine;
using System.Collections;

public class SwipeBooster : MonoBehaviour {

    private Vector3 firstTouch, secondTouch, distance;
    private bool isSwiped;
    public float sensitivity = .2f;
	// Use this for initialization
	void Start () {
        isSwiped = false;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Touch touch in Input.touches) {
            if(touch.phase == TouchPhase.Began) {
                firstTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
            }

            if(touch.phase == TouchPhase.Moved && !isSwiped) {
                secondTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0 ));

                float deltaX = secondTouch.z - firstTouch.z;
                float deltaY = secondTouch.y - firstTouch.y;

                distance = new Vector3(0, deltaY, deltaX);
                bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if(distance != null) {
                    if(/*swipedSideways && */distance.z > sensitivity) {
                        StaminaScript.isBoosting = true; 
                    }
                    else if(/*swipedSideways && */distance.z < -sensitivity) {
                        StaminaScript.isBoosting = false;
                    }
                }
            }
        }
	}
}
