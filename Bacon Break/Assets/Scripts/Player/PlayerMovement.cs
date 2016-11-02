using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // todo: make this script into a singleton

    public float touchSensivity = 1;
    float speed = 0.0f; // character speed on Z axis
    public float baseSpeed = 10f;
    public float bonusSpeed = 30.0f;
    private float maxSpeed;
    public float switchSpeed = 3f; // character switch lane speed on X axis

    int step = 9; // total laneswitch step size
    float totalMovement = 0; // used to store the total distance travelled on the x axis when switching lanes
    int switchDirection = 0; // direction in witch to switch lanes
    float toBeMoved = 0;

    public int deathHeight = -30;

    public static bool isAbleToMove;

    //public bool controlWithButtons;

    bool hold = false; // check if the mouse is holding the player after a mouseclick

    RaycastHit hitInfo; // this raycasts target information contain info if the player has hit a trap

    GameObject theStamina;
    StaminaScript staminaScript;

    // Distance reference points for the swipe release
    Vector3 pos1;
    Vector3 pos2;

    // Use this for initialization
    void Start()
    {
        maxSpeed = 50;
        if (GameObject.Find("M-LVL8_TheHills")){
            baseSpeed = 10.0f;
            bonusSpeed = 25.0f;
        } else {
            baseSpeed = 30.0f;
            bonusSpeed = 35.0f;
        }

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
            if (StaminaScript.isBoosting) {
                //speed = baseSpeed + bonusSpeed * staminaScript.estimatedSpeed;
                speed = maxSpeed;
            }
            else {
                speed = baseSpeed;
            }
            Debug.Log(speed);
            transform.Translate(0, 0, speed * Time.deltaTime);

            if (transform.position.y < deathHeight)
            {
                WinOrLoseScript.isDead = true;
                isAbleToMove = false;
            }

            // switch control scheme for phone or pc debugging
            #if UNITY_EDITOR
                simpleControls();
            #else
                swipeControls();
            #endif

            // switch lane update
            smoothLaneTransition();
        }
    }

    // used to swipe the players between lanes
    void swipeControls()
    {
        if (Input.touchCount > 0)
        {
            var theTouch = Input.GetTouch(0); // all curent touch information

            // touch input
            switch (theTouch.phase)
            {
                case TouchPhase.Began:
                {
                    // send ray from mouse position after mouseclick
                    hitInfo = new RaycastHit();

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(theTouch.position), out hitInfo))
                    {
                        if (hitInfo.transform.gameObject.tag != "Trap" && hitInfo.transform.gameObject.tag != "MoveBridge")
                        {
                            pos1 = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 0));
                            // hold stays true as long as the mousebutton is held
                            hold = true;
                        }
                    }
                    else
                    {
                        pos1 = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 0));
                        // hold stays true as long as the mousebutton is held
                        hold = true;
                    }
                    break;
                }

                case TouchPhase.Moved:
                {
                    // if the mousebutton has not yet been released after clicking on the swipebox
                    if (hold)
                    {
                        // second raycast to check if the player has swiped over the swipebox
                        pos2 = Camera.main.ScreenToWorldPoint(new Vector3(theTouch.position.x, theTouch.position.y, 0));

                        // position of the first hit minus the second hit on the X axis from the player in order to calculate the distance
                        float distance3 = pos1.x - pos2.x;

                        // check if the position of the first point is before the player and the second one after the player to simulate a swiping behaviour
                        // canMove is used to make sure that there arent any walls next to the player before moving there
                        if (distance3 > touchSensivity && canMove(-1))
                        {
                            if (switchDirection == 0)
                            {
                                toBeMoved = step;
                                totalMovement = 0;
                            }

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
                    break;
                }

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

    /*public void MoveCharLeft()
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
    }*/

    // used to detect walls next to the player
    bool canMove(float dir)
    {
        // raycast on the X axis in the direction which the player whishes to move towards
        float dist = 5;
        Vector3 rayDir = new Vector3(dir, 0, 0);
        RaycastHit hit;
        Debug.DrawRay(transform.position, rayDir, Color.green);
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
            transform.Translate(switchDirection * switchSpeed, 0, 0);
            // keep track of the total amount of distance travelled
            totalMovement += switchSpeed;
        }
        // if it does exceed the step size but didnt exactly travelled the step size
        else if (totalMovement != toBeMoved)
        {
            // translate the parent with the remaining distance 
            transform.Translate(switchDirection * (toBeMoved - totalMovement), 0, 0);
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Endpoint")
        {
            WinOrLoseScript.hasWon = true;
        }
    }
}