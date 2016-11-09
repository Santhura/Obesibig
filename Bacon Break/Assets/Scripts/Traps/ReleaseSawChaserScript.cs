using UnityEngine;
using System.Collections;

public class ReleaseSawChaserScript : MonoBehaviour {

    public TrapTap trapScript;
    public HatchScript hatchScript;

    // Use this for initialization
    void Start () {
        trapScript = GameObject.Find("ChasingSawTrap").GetComponent<TrapTap>();
        hatchScript = GameObject.Find("RotateChaseSaw").GetComponent<HatchScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Releases saw forward
        if (other.gameObject.tag == "Player" && gameObject.name == "CollissionBoxSawChaser")
        {
            trapScript.releaseChaser = true;
        }
        
        //Opens hatch 
        if (other.gameObject.tag == "Player" && gameObject.name == "CollisionboxOpenHatch")
        {
            hatchScript.OpenHatch();
        }
    }
}
