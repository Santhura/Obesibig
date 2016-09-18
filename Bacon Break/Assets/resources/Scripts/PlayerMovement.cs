using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.3f;
    public bool swipe = false;
    bool hold = false;
    RaycastHit hitInfo;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0,0,speed);

        if (swipe)
            swipeControls();
        else
            simpelControls();
	}

    void swipeControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                //Debug.Log("Hit " + hitInfo.transform.position);
                if (hitInfo.transform.gameObject.tag == "Player")
                {
                    hold = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (hold)
            {
                Debug.Log("Hit " + hitInfo.transform.position);
                Debug.Log("works");

                // get object point on screen
                // check if mouse position is to the left or the right of the object on release
                if (transform.position.x > -6)
                    transform.Translate(-6, 0, 0);
                //if (transform.position.x < 6)
                //    transform.Translate(6, 0, 0);
            }
            hold = false;
        }
    }

    void simpelControls()
    {
        if (Input.GetKeyDown("a") && transform.position.x > -6)
            transform.Translate(-6, 0, 0);
        if (Input.GetKeyDown("d") && transform.position.x < 6)
            transform.Translate(6, 0, 0);
    }
}
