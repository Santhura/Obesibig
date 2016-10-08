using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceMeterScript : MonoBehaviour 
{
    public Slider sldr_distance;                //The slider displaying the distance traveled.

    private GameObject startPoint, endPoint;    
    private float distanceTraveled;

	// Use this for initialization
	void Start ()
    {
        //Get start and end point and initialize the max slider value.
        startPoint = GameObject.Find("Start_Point");
        endPoint = GameObject.Find("End_Point");
        sldr_distance.maxValue = Vector3.Distance(startPoint.transform.position, endPoint.transform.position) - 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //Update slider value.
        distanceTraveled = Vector3.Distance(transform.position, startPoint.transform.position);
        sldr_distance.value = distanceTraveled;
	}

    void OnCollisionEnter(Collision col)
    {
        //Aw yiss you have reached the end point so gudd.
        if (col.gameObject.tag == "Endpoint")
        {
            WinOrLoseScript.hasWon = true;
            PlayerMovement.isAbleToMove = false;
            //Time.timeScale = 0f;
        }
    }
}
