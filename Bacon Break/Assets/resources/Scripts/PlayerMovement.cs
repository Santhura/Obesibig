using UnityEngine;
using System.Collections;

/*
    Make the player jump instead of teleport between lanes.

    Make the camera follow the player on the Y axis, except for the jumping animation.
    This is so that the player can fall to a lower or move to a higher lane on the Y axis.

    Make the camera center on the road, however do this smoothly, for example:
        - when theres suddenly 5 instead of 2 lanes slowly center the camera on the new center of the lanes.
*/

public class PlayerMovement : MonoBehaviour
{

    public float speed = 0.3f;
    public bool swipe = false;
    bool hold = false;
    RaycastHit hitInfo;
    int step = 6;
    public static bool isAbleToMove;

    // Use this for initialization
    void Start()
    {
        isAbleToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.timeSinceLevelLoad);

        if(isAbleToMove)
            transform.parent.Translate(0, 0, speed * Time.deltaTime);

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
                Vector3 screenPos = Camera.main.WorldToScreenPoint(hitInfo.transform.position);
                float relativePos = screenPos.x - Input.mousePosition.x;
                //Debug.Log("target is " + temp + " pixels from the mouse");

                if (relativePos > 10 && canMove(-1))
                    transform.Translate(-step, 0, 0);
                else if (relativePos < -10 && canMove(1))
                    transform.Translate(step, 0, 0);
            }
            hold = false;
        }
    }

    void simpelControls()
    {
        if (Input.GetKeyDown("a") && canMove(-1))
            transform.Translate(-6, 0, 0);
        if (Input.GetKeyDown("d") && canMove(1))
            transform.Translate(6, 0, 0);
    }

    bool canMove(float dir)
    {
        float dist = 10;
        Vector3 rayDir = new Vector3(dir, 0, 0);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDir, out hit, dist) && hit.collider.gameObject.tag == "Wall")
            return false;
        else
            return true;
        //nothing was next to your gameObject within 10m.
    }
}