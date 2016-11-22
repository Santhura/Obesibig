using UnityEngine;
using System.Collections;

public class CameraEditorScript : MonoBehaviour {

    public bool rot, pos, cameraMode = false;
    public bool canRun = true; //stop and start player bool
    public float originalSpeed;
    private float originalRotX, originalRotY, originalRotZ;
    public Texture aTexture;
    private Transform cameraNewtransform;
    private Transform cameraManipulator;
    private Quaternion targetOriginalRot;
    private Transform targetOriginalPos;
    public Transform lookAtPlayer;
    public GameObject getCamera;
    public GameObject OriginalCamviewObj;
    public GameObject playerObj;

    private float manipulatorSpeed, rotationSpeed;

	// Use this for initialization
	void Start () {
        getCamera = GameObject.Find("Main Camera");
        cameraManipulator = getCamera.transform;
        targetOriginalPos = OriginalCamviewObj.transform;

        lookAtPlayer = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player");

        //  Set original position and rotation
        //  targetOriginalPos.position = OriginalCamviewObj.transform.position;
        originalRotX = transform.rotation.x;
        originalRotY = transform.rotation.y;
        originalRotZ = transform.rotation.z;
        targetOriginalRot = new Quaternion (originalRotX, originalRotY, originalRotZ, 0.0f);

        originalSpeed = playerObj.GetComponent<PlayerMovement>().baseSpeed;
    }
	
	// Update is called once per frame
	void Update () {

        //***** Turn on CAMERA EDITING MODE ****
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.C))
        {
            cameraMode = true;
            //  Show warning text "Camera editing!"

        }   else if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.C) && cameraMode)
            {
                cameraMode = false;
                playerObj.GetComponent<PlayerMovement>().baseSpeed = originalSpeed;
            }
            
            //  Show controls for moving the camera

            
            //  Show how to turn exit mode.
            //  Show how to save positions.

        //  Show reset position or rotation
        if (cameraMode)
        {
            //  Set run speed player to zero or make him run again.
            if (Input.GetKeyDown(KeyCode.L) && playerObj.GetComponent<PlayerMovement>().baseSpeed > 0)
            {
                originalSpeed = playerObj.GetComponent<PlayerMovement>().baseSpeed;
                playerObj.GetComponent<PlayerMovement>().baseSpeed = 0;
            }
            else if (Input.GetKeyDown(KeyCode.K) && playerObj.GetComponent<PlayerMovement>().baseSpeed == 0)
            {
                playerObj.GetComponent<PlayerMovement>().baseSpeed = originalSpeed;
                originalSpeed = 0;
            }

            //  Set original rotation
            if (Input.GetKeyDown(KeyCode.O))
            {
                rot = true;
            }

            if (rot)
            {
                var step = rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, OriginalCamviewObj.transform.rotation, step);

                if (transform.rotation == OriginalCamviewObj.transform.rotation)
                {
                    rot = false;
                }
            }

            //  Set new position
            if (Input.GetKeyDown(KeyCode.I))
            {
                pos = true;
                // Save position in a list position.
            }

            if (pos)
            {
                var step = rotationSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetOriginalPos.position, step);

                if (transform.position == OriginalCamviewObj.transform.position)
                {
                    pos = false;
                }
            }

            //  MOVEMENT CAMERA CONTROLS

            //  Strafe left or right
            if (Input.GetKey(KeyCode.Keypad7))
            {
                Debug.Log("Pressed 4 numeric key");
                gameObject.transform.Translate(Vector3.left * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad9))
            {
                Debug.Log("Pressed 6 numeric key");
                gameObject.transform.Translate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

            //  Go up or down
            if (Input.GetKey(KeyCode.KeypadDivide))
            {
                Debug.Log("Pressed 8 numeric key");
                getCamera.transform.Translate(Vector3.up * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad8))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Translate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }

            //  Rotate to the left or right
            if (Input.GetKey(KeyCode.Keypad5))
            {
                Debug.Log("Pressed 8 numeric key");
                getCamera.transform.Rotate(Vector3.left * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad2))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

            //  Rotate upwards or dowards
            if (Input.GetKey(KeyCode.Keypad1))
            {
                Debug.Log("Pressed 8 numeric key");
                getCamera.transform.Rotate(Vector3.up * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad3))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(0, 0, manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad6))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(0, 0, -manipulatorSpeed * Time.deltaTime);
            }
        }
       
    }   

    void OnGUI()
    {       
        if (cameraMode)
        {
            

            //Background
            GUI.DrawTexture(new Rect(10, 10, 140, 300), aTexture, ScaleMode.StretchToFill, true, 10.0F);
            GUI.Box(new Rect(15, 15, 100, 20), "CAM EDITING");

            //Button Controls
            GUI.Label(new Rect(30, 45, 100, 20), "Stop/Start Player movement");
            if (GUI.Button(new Rect(35, 65, 40, 20), "Stop") && canRun)
            {
                canRun = false;
                StopStartPlayer(canRun);
            }

            if (GUI.Button(new Rect(85, 65, 40, 20), "Start") && !canRun)
            {
                canRun = true;
                StopStartPlayer(canRun);
            }
        }
    }

    void StopStartPlayer(bool canMove)
    {
        if (canMove)
        {
            playerObj.GetComponent<PlayerMovement>().baseSpeed = originalSpeed;
            originalSpeed = 0;
            Debug.Log("Started the player move");
        }

        if (!canMove)
        {
            originalSpeed = playerObj.GetComponent<PlayerMovement>().baseSpeed;
            playerObj.GetComponent<PlayerMovement>().baseSpeed = 0;
            Debug.Log("Stopped the player move");
        }
    }
}
