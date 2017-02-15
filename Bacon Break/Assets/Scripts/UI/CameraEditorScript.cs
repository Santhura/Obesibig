using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CameraEditorScript : MonoBehaviour {

    public static CameraEditorScript camEditor;

    //singleton
    void Awake()
    {
        if (camEditor == null)
        {
            camEditor = this;
        } else if (camEditor != this)
        {
            Destroy(gameObject);
        }
    }

    //Camera translation and rotations keys and variables
    public bool cameraMode = false;
    private GameObject playerObj;
    private bool canRun = true; //stop and start player bool
    private bool translateUP, translateDOWN, translateRIGHT, translateLEFT, rotateUP, rotateDOWN, rotateLEFT, rotateRIGHT, rotateLeftClockwise, rotateRightClockwise, resetPos = false;
    public bool rot, pos, loadData;
    public float originalSpeed, stopLoader;
    private float originalRotX, originalRotY, originalRotZ;
    private float manipulatorSpeed, resetCamSpeed;

    //position and rotation variables for loading or saving
    private Transform originalTransform, preSet1;
    private Quaternion preSet1Angle;
    private Quaternion loadQuartRotSet1;
    private Quaternion targetOriginalRot;
    private Vector3 loadNewPos;

    //camera vieuwpoint and transforms variables
    private Transform cameraNewtransform;
    private Transform cameraManipulator;
    private Transform targetOriginalPos;
    private Transform lookAtPlayer;
    private GameObject getCamera;
    public GameObject originalCamviewObj;

    //UI Layout
    public Texture aTexture;
    public GUIStyle CamEdStyle;





	// Use this for initialization
	void Start () {
        manipulatorSpeed = 30;
        resetCamSpeed = 5;

        getCamera = GameObject.Find("Main Camera");
        originalCamviewObj = GameObject.Find("CamOriginalPos");
        targetOriginalPos = originalCamviewObj.transform;
        cameraManipulator = getCamera.transform;

        lookAtPlayer = GameObject.Find("Player").transform;
        playerObj = GameObject.Find("Player");

        preSet1 = gameObject.transform;

        //  Set original position and rotation
        originalRotX = transform.rotation.x;
        originalRotY = transform.rotation.y;
        originalRotZ = transform.rotation.z;
        targetOriginalRot = new Quaternion (originalRotX, originalRotY, originalRotZ, 0.0f);

        originalSpeed = playerObj.GetComponent<PlayerMovement>().baseSpeed;

        print(Application.persistentDataPath);
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

            //  Set camera back to its original rotation
            if (Input.GetKeyDown(KeyCode.O))
            {
                rot = true;
            }

            if (rot)
            {
                var step = resetCamSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, originalCamviewObj.transform.rotation, step);

                if (transform.rotation == originalCamviewObj.transform.rotation)
                {
                    rot = false;
                }
            }

            //LOADING PRESET TRANSFORM
            if (loadData)
            {
                var step = resetCamSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, loadQuartRotSet1, step);
                transform.position = Vector3.Lerp(transform.position, loadNewPos, step);
                

                //FIX ME - temp solotion
                 stopLoader++;
                if (stopLoader >= 60)
                {
                    loadData = false;
                    stopLoader = 0;
                }
                
            }

            //  Set camera back to its new position
            if (Input.GetKeyDown(KeyCode.I))
            {
                pos = true;
                // Save position in a list position.
            }
            
            if (pos)
            {
                var step = resetCamSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetOriginalPos.position, step);

                /*
                var orPos = Mathf.Round(targetOriginalPos.transform.position.x);
                var curPos = Mathf.Round(transform.position.x);
                Debug.Log(orPos);
                Debug.Log(orPos);
                if (curPos ==  orPos)
                {
                    pos = false;
                    Debug.Log("Done reposition");
                }*/

                //FIX ME - temp solotion
                stopLoader++;
                if (stopLoader >= 60)
                {
                    pos = false;
                    stopLoader = 0;
                }
            }

            //  MANUAL (numeric keypad) MOVEMENT CAMERA CONTROLS

            //  Translate left or right
            if (Input.GetKey(KeyCode.Keypad7) || translateLEFT)
            {
                gameObject.transform.Translate(Vector3.left * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad9) || translateRIGHT)
            {
                gameObject.transform.Translate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

            //  Translate up or down
            if (Input.GetKey(KeyCode.KeypadDivide) || translateUP)
            {
                getCamera.transform.Translate(Vector3.up * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad8) || translateDOWN)
            {
                getCamera.transform.Translate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }

            //  Rotate to the left or right
            if (Input.GetKey(KeyCode.Keypad5) || rotateUP)
            {
                getCamera.transform.Rotate(Vector3.left * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad2)|| rotateDOWN)
            {
                getCamera.transform.Rotate(Vector3.right * manipulatorSpeed * Time.deltaTime);
            }

            //  Rotate upwards or dowards
            if (Input.GetKey(KeyCode.Keypad1)|| rotateLEFT)
            {
                getCamera.transform.Rotate(Vector3.up * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad3)|| rotateRIGHT)
            {
                getCamera.transform.Rotate(Vector3.down * manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad4) || rotateLeftClockwise)
            {
                getCamera.transform.Rotate(0, 0, manipulatorSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Keypad6)|| rotateRightClockwise)
            {
                getCamera.transform.Rotate(0, 0, -manipulatorSpeed * Time.deltaTime);
            }
        }
       
    }   

    void OnGUI()
    {
        CamEdStyle = new GUIStyle();
        CamEdStyle.normal.textColor = Color.red;
        CamEdStyle.fontStyle = FontStyle.Bold;

        if (cameraMode)
        {

            //CAMERA EDITOR BACKGROUND
            GUI.DrawTexture(new Rect(10, 10, 150, 300), aTexture, ScaleMode.StretchToFill, true, 10.0F);
            GUI.Box(new Rect(40, 20, 100, 20), "CAM EDITING", CamEdStyle);

            //PLAYER MOVEMENT BUTTONS
            GUI.Label(new Rect(30, 45, 100, 20), "Player speed L/K");
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

            //TRANSLATION BUTTONS
            GUI.Label(new Rect(30, 85, 150, 20), "Translate (7,8,9,/)");
            if (GUI.Button(new Rect(60, 105, 45, 20), "Up"))
            {
                translateUP = true;
            }
            else{
                translateUP = false;
            }

            if (GUI.Button(new Rect(60, 125, 45, 20), "Down"))
            {
                translateDOWN = true;
            }
            else {
                translateDOWN = false;
            }

            if (GUI.Button(new Rect(25, 115, 35, 20), "Left"))
            {
                translateLEFT = true;
            }
            else {
                translateLEFT = false;
            }

            if (GUI.Button(new Rect(105, 115, 40, 20), "Right"))
            {
                translateRIGHT = true;
            }
            else {
                translateRIGHT = false;
            }

            //ROTATIONAL BUTTONS
            GUI.Label(new Rect(30, 145, 150, 20), "Rotate (1,2,3,5,4,6)");
            if (GUI.Button(new Rect(60, 165, 45, 20), "Up"))
            {
                rotateUP = true;
            }
            else {
                rotateUP = false;
            }

            if (GUI.Button(new Rect(60, 185, 45, 20), "Down"))
            {
                rotateDOWN = true;
            }
            else {
                rotateDOWN = false;
            }

            if (GUI.Button(new Rect(25, 185, 35, 20), "Left"))
            {
                rotateLEFT = true;
            }
            else {
                rotateLEFT = false;
            }

            if (GUI.Button(new Rect(105, 185, 40, 20), "Right"))
            {
                rotateRIGHT = true;
            }
            else {
                rotateRIGHT = false;
            }

            if (GUI.Button(new Rect(105, 165, 49, 20), "Clck-R"))
            {
                rotateRightClockwise = true;
            }
            else {
                rotateRightClockwise = false;
            }

            if (GUI.Button(new Rect(13, 165, 49, 20), "Clck-L"))
            {
                rotateLeftClockwise = true;
            }
            else {
                rotateLeftClockwise = false;
            }

            //RESET POSITION & ROTATION
            GUI.Label(new Rect(30, 205, 150, 20), "Reset transfrom O/I");
            if (GUI.Button(new Rect(30, 225, 49, 20), "R-Pos"))
            {
                pos = true;
            }
            if (GUI.Button(new Rect(85, 225, 49, 20), "R-Rot"))
            {
                rot = true;
            }

            //SAVING BUTTONS
            GUI.Label(new Rect(30, 245, 150, 20), "Save new positions");
            if (GUI.Button(new Rect(15, 265, 49, 20), "Set1"))
            {
                preSet1.transform.position = gameObject.transform.position;
                preSet1.transform.eulerAngles = gameObject.transform.eulerAngles;
                Debug.Log("Preset has been temperarly saved.");
            }

            if (GUI.Button(new Rect(75, 285, 49, 20), "Load1"))
            {
                gameObject.transform.position = preSet1.transform.position;
                gameObject.transform.eulerAngles = preSet1.transform.eulerAngles;
            }

            if (GUI.Button(new Rect(115, 300, 49, 20), "Load"))
            {
                Load(1);
            }

            if (GUI.Button(new Rect(15, 300, 49, 20), "Save"))
            {
                Save();
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

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

            if (File.Exists(Application.persistentDataPath + "/cameraInfo.dat"))
        {
            Debug.Log("This file exicst");
            FileStream file = File.Open(Application.persistentDataPath + "/cameraInfo.dat", FileMode.Append, FileAccess.Write);
            //open existing file and work on it
            CameraData data = new CameraData();

            data.preSet1PosX = preSet1.position.x;
            data.preSet1PosY = preSet1.position.y;
            data.preSet1PosZ = preSet1.position.z;
            data.preSet1RotX = preSet1.eulerAngles.x;
            data.preSet1RotY = preSet1.eulerAngles.y;
            data.preSet1RotZ = preSet1.eulerAngles.z;

            //data.preSet2PosX = preSet2.position.x;
            //data.preSet2PosY = preSet2.position.y;
            //data.preSet2PosZ = preSet2.position.z;
            //data.preSet2RotX = preSet2.eulerAngles.x;
            //data.preSet2RotY = preSet2.eulerAngles.y;
            //data.preSet2RotZ = preSet2.eulerAngles.z;

            //data.preSet3PosX = preSet3.position.x;
            //data.preSet3PosY = preSet3.position.y;
            //data.preSet3PosZ = preSet3.position.z;
            //data.preSet3RotX = preSet3.eulerAngles.x;
            //data.preSet3RotY = preSet3.eulerAngles.y;
            //data.preSet3RotZ = preSet3.eulerAngles.z;

            bf.Serialize(file, data);
            file.Close();

            Debug.Log("Saved data");
        } else
            {
            FileStream file = File.Create(Application.persistentDataPath + "/cameraInfo.dat");
            file.Close();
            Debug.Log("Created File");
        }
    }


    public void Load(float loadPresetNr)
    {
        if (File.Exists(Application.persistentDataPath + "/cameraInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/cameraInfo.dat", FileMode.Open);
            CameraData data = (CameraData)bf.Deserialize(file);
            file.Close();

            if (loadPresetNr == 1)
            {
                var trsX = data.preSet1PosX;
                var trsY = data.preSet1PosY;
                var trsZ = data.preSet1PosZ;
                preSet1.transform.position = new Vector3(trsX, trsY, trsZ);
                preSet1Angle = Quaternion.Euler(data.preSet1RotX, data.preSet1RotY, data.preSet1RotZ);
                Debug.Log(preSet1.transform.position);
            }

            //preSet1.transform.position = new Vector3(data.preSet1PosX, data.preSet1PosY, data.preSet1PosZ);



            /*
            if (loadPresetNr == 1)
            {
                //Position
                Vector3 loadPosition = new Vector3(data.preSet1PosX, data.preSet1PosY, data.preSet1PosZ);
                loadNewPos = loadPosition;

                //Rotation
                Quaternion newAngle = Quaternion.Euler(data.preSet1RotX, data.preSet1RotY, data.preSet1RotZ);
                loadQuartRotSet1 = newAngle;
                loadData = true;

                Debug.Log("Loaded set 1");
            } */

        }
    }
}

[Serializable]
class CameraData
{
    public float preSet1PosX;
    public float preSet1PosY;
    public float preSet1PosZ;
    public float preSet1RotX;
    public float preSet1RotY;
    public float preSet1RotZ;

}
