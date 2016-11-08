using UnityEngine;
using System.Collections;

public class HatchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenHatch()
    {
        //Open Hatch
        iTween.MoveAdd(GameObject.Find("ChasingSawTrap"), iTween.Hash("amount", new Vector3(5.5f, 0, 0), "time", 1f, "easetype", iTween.EaseType.linear));
        iTween.RotateAdd(GameObject.Find("RotateChaseSaw"), iTween.Hash("amount", new Vector3(-90, 0, 0), "time", 2f, "easetype", iTween.EaseType.linear));
    }
}
