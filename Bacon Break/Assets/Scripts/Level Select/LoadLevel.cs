using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
    //public string thisLevel;
    private GameObject loadLevel;

	// Use this for initialization
	void Awake () {
        //thisLevel = PlayerPrefs.GetString("level");
        LoadMyLevel();
      
    }
    void LoadMyLevel()
    {
        loadLevel = (GameObject)Instantiate(Resources.Load("Levels/" + UIManager.currentLevelName));

        SetCameraView();
    }

    //Change main camera position and rotation based on the given settings
    void SetCameraView()
    {
        GameObject mainCamera = GameObject.Find("Main Camera");

        switch (PlayerPrefs.GetInt("CameraView"))
        {
            /* Orthographic camera:
             * - The view we normally have, sort of isometric/2.5D view.
             */
            case 0:
                mainCamera.transform.localPosition = new Vector3(34.39f, 33.25f, -21.13f);
                mainCamera.transform.localRotation = Quaternion.Euler(35.393f, -48.488f, -1.854f);
                mainCamera.GetComponent<Camera>().orthographic = true;
                break;
            /* Perspective camera:
             * - Sort of like the orthographic camera, but then in perspective 
             * so the player is able to look further ahead.
             * ... Why did we even choose orthographic in the first place?
             */
            case 1:
                mainCamera.transform.localPosition = new Vector3(7.9f, 10.22f, 1.44f);
                mainCamera.transform.localRotation = Quaternion.Euler(43.05f, -47.5f, 5.832f);
                mainCamera.GetComponent<Camera>().orthographic = false;
                break;
            /* Perspective camera:
             * - Seen from the perspective of the pig (sort of);
             * - Gives a better view of the level in general
             * - You are able to give a good perception of speed (the camera could move slightly backwards)
             */
            case 2:
                mainCamera.transform.localPosition = new Vector3(0.0f, 7.0f, -5f);
                mainCamera.transform.localRotation = Quaternion.Euler(32.74f, 0, 0);
                mainCamera.GetComponent<Camera>().orthographic = false;
                break;
            /*
             * FEEL FREE TO ADD MORE OPTIONS
             */
            default:
                //Default camera setting. 
                mainCamera.transform.localPosition = new Vector3(34.39f, 33.25f, -21.13f);
                mainCamera.transform.localRotation = Quaternion.Euler(35.393f, -48.488f, -1.854f);
                mainCamera.GetComponent<Camera>().orthographic = true;
                break;
        }
        /*GameObject mainCamera = GameObject.Find("Main Camera");

        if (OptionScript.IsCameraOrthographic())
        {
            mainCamera.GetComponent<Camera>().orthographic = true;
        }
        else
        {
            mainCamera.GetComponent<Camera>().orthographic = false;
        }

        mainCamera.transform.localPosition = OptionScript.GetCameraPosition();
        mainCamera.transform.localRotation = Quaternion.Euler(OptionScript.GetCameraRotation());
         * */
    }
	
}
