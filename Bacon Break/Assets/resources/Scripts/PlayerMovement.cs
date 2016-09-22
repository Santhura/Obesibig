using UnityEngine;
using System.Collections;

/* To do:
    - Make the player jump instead of teleport between lanes.

    - Make the camera follow the player on the Y axis, except for the jumping animation.
    - This is so that the player can fall to a lower or move to a higher lane on the Y axis.

    - Make the camera center on the road, however do this smoothly, for example:
        > when theres suddenly 5 instead of 2 lanes slowly center the camera on the new center of the lanes.
*/

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.3f; // character speed on Z axis

    public float switchSpeed = 3f; // character switch lane speed on X axis
    int step = 6; // total laneswitch step size
    float totalMovement; // used to store the total distance travelled on the x axis when switching lanes
    int switchDirection; // direction in witch to switch lanes

    public bool swipe = true; // enable swipe controls for mobile phone or disable them for debugging A/D keys
                              // note: make sure to dissable the swipe boolean before building the project
    bool hold = false; // check if the mouse is holding the player after a mouseclick

    RaycastHit hitInfo; // raycast target information containing the player

    // Distance reference points for the swipe release
    Vector3 posRight;
    Vector3 posLeft;

    // Use this for initialization
    void Start()
    {
        totalMovement = step;
    }

    // Update is called once per frame
    void Update()
    {
        // movement speed on Z axis
        transform.parent.Translate(0, 0, speed * Time.deltaTime);

        // switch control scheme for phone or pc debugging
        if (swipe)
            swipeControls();
        else
            simpleControls();

        // switch lane update
        smoothLaneTransition();
    }

    // used to swipe the players between lanes
    void swipeControls()
    {
        //var theTouch = Input.GetTouch(0); // all curent touch information

        // left mousebutton click
        if (Input.GetMouseButtonDown(0))
        // touch input
        //if (theTouch.phase == TouchPhase.Began)
        {
            // send ray from mouse position after mouseclick
            hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition/*theTouch.position*/), out hitInfo);
            if (hit)
            {
                // check if you have clicked on the player
                if (hitInfo.transform.gameObject.tag == "Player")
                {
                    // hold stays true as long as the mousebutton is held
                    hold = true;
                }
            }
        }
        
        // left mouse button release
        if (Input.GetMouseButtonUp(0))
        // release touchpad
        //if (theTouch.phase == TouchPhase.Ended)
        {
            // if the mousebutton hadnt released before after having clicked on the player
            if (hold)
            {
                // reference points for swipe direction
                posRight = new Vector3(15, transform.position.y, transform.position.z);
                posLeft = new Vector3(-15, transform.position.y, transform.position.z);

                // calculate distance between the touched/clicked position and the reference points
                Vector3 temp1 = Camera.main.WorldToScreenPoint(posLeft);
                float dist1 = Vector3.Distance(Input.mousePosition/*theTouch.position*/, temp1);

                Vector3 temp2 = Camera.main.WorldToScreenPoint(posRight);
                float dist2 = Vector3.Distance(Input.mousePosition/*theTouch.position*/, temp2);

                // check if the distance to one point is small enough to simulate a swipe movement and then move the player
                // canMove is used to make sue that there arent any walls next to the player before moving there
                if (dist1 < 250 && canMove(-1) && totalMovement == step)
                {
                    totalMovement = 0;
                    switchDirection = -1;
                }
                //transform.Translate(-step, 0, 0);
                else if (dist2 < 250 && canMove(1) && totalMovement == step)
                {
                    totalMovement = 0;
                    switchDirection = 1;
                }
            }
            // release hold onto the player
            hold = false;
        }
    }

    // used to move the player between lanes
    // note: ment for debugging purposes only, move with A and D keys
    void simpleControls()
    {
        if (Input.GetKeyDown("a") && canMove(-1) && totalMovement == step)
        {
            totalMovement = 0;
            switchDirection = -1;
        }
        else if (Input.GetKeyDown("d") && canMove(1) && totalMovement == step)
        {
            totalMovement = 0;
            switchDirection = 1;
        }
    }

    // used to detect walls next to the player
    bool canMove(float dir)
    {
        // raycast on the X axis in the direction which the player whishes to move towards
        float dist = 10;
        Vector3 rayDir = new Vector3(dir, 0, 0);
        RaycastHit hit;

        // check for a valid raycast hit
        if (Physics.Raycast(transform.position, rayDir, out hit, dist) && hit.collider.gameObject.tag == "Wall")
            // a wall has been detected next to the player
            return false;
        else
            return true;
            // no wall was next to the player within range.
    }

    // used for smooth lane transitioning
    void smoothLaneTransition()
    {
        // check if the total distance plus the speed doesnt exceed the maximum step size
        if (totalMovement + switchSpeed < step)
        {
            // translate the parent object to move at set speed in a direction
            transform.parent.Translate(switchDirection * switchSpeed, 0, 0);
            // keep track of the total amount of distance travelled
            totalMovement += switchSpeed;
        }
        // if it does exceed the step size but didnt exactly travelled the step size
        else if (totalMovement != step)
        {
            // translate the parent with the remaining distance 
            transform.parent.Translate(switchDirection * (step - totalMovement), 0, 0);
            // set total distance travelled to be the total step size
            totalMovement = step;
        }

    }
}