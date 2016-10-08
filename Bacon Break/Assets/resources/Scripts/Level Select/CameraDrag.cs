using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject worldBB;

    //For mouse
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Drag camera with touch
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);

            //Clamp camera so it won't go over the edges of the map
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 5, 10),
                transform.position.y,
                Mathf.Clamp(transform.position.y, -10, 0));
        }*/

        //Drag camera with mouse (for debug purposes)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, 0, pos.y * -dragSpeed);

        transform.Translate(move, Space.World);

        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 10, 10),
                transform.position.y,
                Mathf.Clamp(transform.position.y, -10, 0));

    }
}
