using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
    public string thisLevel;
    GameObject loadLevel;
	// Use this for initialization
	void Start () {
        thisLevel = PlayerPrefs.GetString("level");
        LoadMyLevel();
      
    }
    void LoadMyLevel()
    {
        loadLevel = (GameObject)Instantiate(Resources.Load("Prefabs/Levels/" + thisLevel));
    }
	
}
