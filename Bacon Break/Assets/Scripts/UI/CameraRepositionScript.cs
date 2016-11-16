using UnityEngine;
using System.Collections;

public class CameraRepositionScript : MonoBehaviour {

    public bool tunnelLv8 = false;
    private float originalSpeed;
    public float originalRotX, originalRotY, originalRotZ;
    private Transform cameraNewtransform;
    private Transform cameraManipulator;
    public Transform lookAtPlayer;
    public GameObject getCamera;
    public GameObject playerObj;

    public float manipulatorSpeed;

	// Use this for initialization
	void Start () {
        getCamera = GameObject.Find("Main Camera");
        cameraManipulator = getCamera.transform;

        lookAtPlayer = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player");

        //  Set original position and rotation
        originalRotX = transform.rotation.x;
        originalRotY = transform.rotation.y;
        originalRotZ = transform.rotation.z;
    }
	
	// Update is called once per frame
	void Update () {

        //transform.LookAt(lookAtPlayer);

        //***** Turn on CAMERA EDITING MODE ****

        //  Show warning text "Camera editing!"
        //  Show controls for moving the camera
        //  Show how to turn exit mode.
        //  Show how to save positions.

        //  Show reset position or rotation

        //  Set run speed player to zero or make him run again.
        if (Input.GetKeyDown(KeyCode.L) && playerObj.GetComponent<PlayerMovement>().baseSpeed > 0)
        {
            originalSpeed = playerObj.GetComponent<PlayerMovement>().baseSpeed;
            playerObj.GetComponent<PlayerMovement>().baseSpeed = 0;
        }   else if (Input.GetKeyDown(KeyCode.K) && playerObj.GetComponent<PlayerMovement>().baseSpeed == 0)
            {
                playerObj.GetComponent<PlayerMovement>().baseSpeed = originalSpeed;
                originalSpeed = 0;
            }

        //  Set original rotation
        if (Input.GetKeyDown(KeyCode.O))
        {
            Quaternion.Euler(originalRotX, originalRotY, originalRotZ);
        }

        //  Set new position
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(originalRotX);
            Debug.Log(originalRotY);
            Debug.Log(originalRotZ);
            // Save position in a list position.

        }

        //  Movement Camera controls



        //Strafe left or right
        if (Input.GetKey(KeyCode.Keypad7))
        {
            Debug.Log("Pressed 4 numeric key");
            gameObject.transform.Translate(Vector3.left * manipulatorSpeed * Time.deltaTime);
        }   else if (Input.GetKey(KeyCode.Keypad9))
            {
                Debug.Log("Pressed 6 numeric key");
                gameObject.transform.Translate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

        //Go up or down
        if (Input.GetKey(KeyCode.KeypadDivide))
        {
            Debug.Log("Pressed 8 numeric key");
            getCamera.transform.Translate(Vector3.up * manipulatorSpeed * Time.deltaTime);
        }   else if (Input.GetKey(KeyCode.Keypad8))
            {
                Debug.Log("Pressed 2 numeric key");
            getCamera.transform.Translate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }

        //Rotate to the left or right
        if (Input.GetKey(KeyCode.Keypad5))
        {
            Debug.Log("Pressed 8 numeric key");
            getCamera.transform.Rotate(Vector3.left * manipulatorSpeed * Time.deltaTime);
        }   else if (Input.GetKey(KeyCode.Keypad2))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

        //Rotate upwards or dowards
        if (Input.GetKey(KeyCode.Keypad1))
        {
            Debug.Log("Pressed 8 numeric key");
            getCamera.transform.Rotate(Vector3.up * manipulatorSpeed * Time.deltaTime);
        }   else if (Input.GetKey(KeyCode.Keypad3))
            {
                Debug.Log("Pressed 2 numeric key");
                getCamera.transform.Rotate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }   else if (Input.GetKey(KeyCode.Keypad4))
                {
                    Debug.Log("Pressed 2 numeric key");
                    getCamera.transform.Rotate(0, 0, manipulatorSpeed * Time.deltaTime);
                }   else if (Input.GetKey(KeyCode.Keypad6))
                    {
                        Debug.Log("Pressed 2 numeric key");
                        getCamera.transform.Rotate(0, 0, -manipulatorSpeed * Time.deltaTime);
                    }

    }   

}
