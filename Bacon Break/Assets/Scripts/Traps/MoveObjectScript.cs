using UnityEngine;
using System.Collections;

public class MoveObjectScript : MonoBehaviour {

    float speed = .3f;
    private RaycastHit hit;
    public Renderer rend;
    AudioSource audioSource;


    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
    
        // switch control scheme for phone or pc debugging
        #if UNITY_EDITOR
                simpleMoveObjectControls();
        #else
                swipeObjectControls();
        #endif
	}

    private void simpleMoveObjectControls()
    {

    }

    private void swipeObjectControls()
    {
        // get touch position and check if the right object is hit to move it.
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "MoveBridge")
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                    Vector2 touchPos = Input.GetTouch(0).deltaPosition;

                    hit.transform.Translate(touchPos.x * speed, 0, 0);

                    audioSource.Play();
                }
            }
        }
    }

}
