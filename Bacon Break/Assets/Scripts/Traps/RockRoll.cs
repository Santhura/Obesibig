using UnityEngine;
using System.Collections;

public class RockRoll : MonoBehaviour {
    public float velocity;
    public GameObject child;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((Vector3.right * velocity) * Time.deltaTime);
        child.transform.Rotate(0, 0, -180 * Time.deltaTime);

        if(this.transform.position.y <= 4f)
        {
            transform.rotation = Quaternion.identity;
            Debug.Log("rock not falling.");
        }
    }
}
