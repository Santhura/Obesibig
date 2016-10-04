using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float touchSensivity = 1;
    float speed = 0.0f; // character speed on Z axis
    public float baseSpeed = 20f;
    public float switchSpeed = 3f; // character switch lane speed on X axis

    int step = 9; // total laneswitch step size
    float totalMovement = 0; // used to store the total distance travelled on the x axis when switching lanes
    int switchDirection = 0; // direction in witch to switch lanes
    float toBeMoved = 0;
    int moveDelay = 10;
    int oldDirection;

    public static bool isAbleToMove;

    public bool controlWithButtons;
    public bool swipe = true; // enable swipe controls for mobile phone or disable them for debugging A/D keys
                              // note: make sure to dissable the swipe boolean before building the project
    bool hold = false; // check if the mouse is holding the player after a mouseclick

    RaycastHit hitInfo; // raycast target information containing the player
    RaycastHit secondHitInfo;

    public GameObject theStamina;
    public StaminaScript staminaScript;

    // Distance reference points for the swipe release
    Vector3 posRight;
    Vector3 posLeft;

    // Use this for initialization
    void Start()
    {
        GameObject theStamina = GameObject.Find("bar_stamina");
        staminaScript = theStamina.GetComponent<StaminaScript>();
        
        isAbleToMove = true;
        transform.position = new Vector3(GameObject.Find("Start_Point").transform.position.x, GameObject.Find("Start_Point").transform.position.y + 1, GameObject.Find("Start_Point").transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAbleToMove)
        {
            speed = baseSpeed + baseSpeed * staminaScript.estimatedSpeed;
            transform.parent.Translate(0, 0, speed * Time.deltaTime);
        }

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
        if (Input.touchCount > 0)
        {
            var theTouch = Input.GetTouch(0); // all curent touch information
            moveDelay--;

            // left mousebutton click
            //if (Input.GetMouseButtonDown(0))
            // touch input
            switch (theTouch.phase)
            {
                //if (theTouch.phase == TouchPhase.Began)
                case TouchPhase.Began:
                {
                    // send ray from mouse position after mouseclick
                    hitInfo = new RaycastHit();
                    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(/*Input.mousePosition*/theTouch.position), out hitInfo);
                    if (hit)
                    {
                        // check if you have clicked on the player
                        if (hitInfo.transform.gameObject.tag == "Player")
                        {
                            // hold stays true as long as the mousebutton is held
                            hold = true;
                        }
                    }
                    break;
                }

                case TouchPhase.Moved:
                {
                    // if the mousebutton has not yet been released after clicking on the swipebox
                    if (hold)
                    {
                        // second raycast to check if the player has swiped over the swipebox
                        secondHitInfo = new RaycastHit();
                        bool secondHit = Physics.Raycast(Camera.main.ScreenPointToRay(/*Input.mousePosition*/theTouch.position), out secondHitInfo);

                        // position of the first hit minus the second hit on the X axis from the player in order to calculate the distance
                        float distance3 = hitInfo.point.x - secondHitInfo.point.x;

                        // check if the position of the first point is before the player and the second one after the player to simulate a swiping behaviour
                        // canMove is used to make sure that there arent any walls next to the player before moving there
                        if (distance3 > touchSensivity && canMove(-1))
                        {
                            if (switchDirection == 0)
                            {
                                toBeMoved = step;
                                totalMovement = 0;
                            }
                            /*else if (switchDirection == -1)
                            {
                                //toBeMoved += step;
                            }*/
                            else if (switchDirection == 1)
                            {
                                totalMovement = totalMovement % step;
                                totalMovement = step - totalMovement;
                                toBeMoved = step;
                            }

                            switchDirection = -1;
                            hold = false;
                        }
                        else if (distance3 < -touchSensivity && canMove(1))
                        {
                            if (switchDirection == 0)
                            {
                                toBeMoved = step;
                                totalMovement = 0;
                            }
                            /*else if (switchDirection == 1)
                            {
                                //toBeMoved += step;
                            }*/
                            else if (switchDirection == -1)
                            {
                                totalMovement = totalMovement % step;
                                totalMovement = step - totalMovement;
                                toBeMoved = step;
                            }

                            switchDirection = 1;
                            hold = false;
                        }
                    }
                    /*else /*if (switchDirection != oldDir /*|| moveDelay <=0
                    {
                        // send ray from mouse position after mouseclick
                        hitInfo = new RaycastHit();
                        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(/*Input.mousePositiontheTouch.position), out hitInfo);
                        if (hit)
                        {
                            // check if you have clicked on the player
                            if (hitInfo.transform.gameObject.tag == "Player")
                            {
                                // hold stays true as long as the mousebutton is held
                                hold = true;
                            }
                        }
                    }*/
                    break;
                }

                //case TouchPhase.Stationary:
                //{
                //    if (!hold)
                //    {
                //        // send ray from mouse position after mouseclick
                //        hitInfo = new RaycastHit();
                //        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(/*Input.mousePosition*/theTouch.position), out hitInfo);
                //        if (hit)
                //        {
                //            // check if you have clicked on the player
                //            if (hitInfo.transform.gameObject.tag == "Player")
                //            {
                //                // hold stays true as long as the mousebutton is held
                //                hold = true;
                //            }
                //        }
                //    }
                //    break;
                //}

                // left mouse button release
                //if (Input.GetMouseButtonUp(0))
                // release touchpad
                //if (theTouch.phase == TouchPhase.Ended)
                case TouchPhase.Ended:
                {
                    // release hold onto the player
                    hold = false;
                    break;
                }
            }
        }
    }

    // used to move the player between lanes
    // note: ment for debugging purposes only, move with A and D keys
    void simpleControls()
    {
        if (Input.GetKeyDown("a") && canMove(-1))
        {
            if (switchDirection == 0)
            {
                toBeMoved = step;
                totalMovement = 0;
            }
            else if (switchDirection == -1)
            {
                toBeMoved += step;
            }
            else if (switchDirection == 1)
            {
                totalMovement = totalMovement % step;
                totalMovement = step - totalMovement;
                toBeMoved = step;
            }

            switchDirection = -1;
        }
        else if (Input.GetKeyDown("d") && canMove(1))
        {
            if (switchDirection == 0)
            {
                toBeMoved = step;
                totalMovement = 0;
            }
            else if (switchDirection == 1)
            {
                toBeMoved += step;
            }
            else if (switchDirection == -1)
            {
                totalMovement = totalMovement % step;
                totalMovement = step - totalMovement;
                toBeMoved = step;
            }

            switchDirection = 1;
        }
    }

    public void MoveCharLeft()
    {
        if (controlWithButtons)
        {
            totalMovement = 0;
            switchDirection = -1;
        }
    }
    public void MoveCharRight()
    {
        if (controlWithButtons)
        {
            totalMovement = 0;
            switchDirection = 1;
        }
    }

    // used to detect walls next to the player
    bool canMove(float dir)
    {
        // raycast on the X axis in the direction which the player whishes to move towards
        float dist = 5;
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
        if (totalMovement + switchSpeed < toBeMoved)
        {
            // translate the parent object to move at set speed in a direction
            transform.parent.Translate(switchDirection * switchSpeed, 0, 0);
            // keep track of the total amount of distance travelled
            totalMovement += switchSpeed;
        }
        // if it does exceed the step size but didnt exactly travelled the step size
        else if (totalMovement != toBeMoved)
        {
            // translate the parent with the remaining distance 
            transform.parent.Translate(switchDirection * (toBeMoved - totalMovement), 0, 0);
            // set total distance travelled to be the total step size
            totalMovement = 0;
            switchDirection = 0;
        }
        else
        {
            totalMovement = 0;
            switchDirection = 0;
        }

    }
}