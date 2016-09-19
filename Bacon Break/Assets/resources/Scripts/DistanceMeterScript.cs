using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceMeterScript : MonoBehaviour {

    public Text txt_distance;

    private GameObject endPoint;
    private float distanceToEndPoint;

	// Use this for initialization
	void Start ()
    {
        endPoint = GameObject.Find("End_Point");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        distanceToEndPoint = Vector3.Distance(transform.position, endPoint.transform.position);
        txt_distance.text = "Distance to target: " + (int)distanceToEndPoint;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Endpoint")
        {
            txt_distance.text = "Distance to target: 0";
            Time.timeScale = 0f;
        }
    }
}
