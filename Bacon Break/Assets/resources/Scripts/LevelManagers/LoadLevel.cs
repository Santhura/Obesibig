using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
    public string thisLevel;
    private GameObject loadLevel;

	// Use this for initialization
	void Awake () {
        thisLevel = PlayerPrefs.GetString("level");
        LoadMyLevel();
      
    }
    void LoadMyLevel()
    {
        loadLevel = (GameObject)Instantiate(Resources.Load("Prefabs/Levels/" + thisLevel));

        //Change main camera position and rotation based on the given settings
        GameObject mainCamera = GameObject.Find("Main Camera");
        //mainCamera.transform.position = OptionScript.GetCameraPosition();
        mainCamera.transform.rotation = Quaternion.Euler(OptionScript.GetCameraRotation());
    }
	
}
