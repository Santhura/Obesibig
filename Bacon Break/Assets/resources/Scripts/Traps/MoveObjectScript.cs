using UnityEngine;
using System.Collections;

public class MoveObjectScript : MonoBehaviour {

    float speed = .5f;
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // get touch position and check if the right object is hit to move it.
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "MoveBridge")
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = Input.GetTouch(0).deltaPosition;

                    hit.transform.parent.Translate(touchPos.x * speed, 0, 0);
                }
            }
        }
	}


}
