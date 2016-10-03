using UnityEngine;
using System.Collections;

public class TapTutorial : MonoBehaviour {
    public float movementSpeed = 100.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += transform.right * Time.deltaTime * movementSpeed;
    }
}
