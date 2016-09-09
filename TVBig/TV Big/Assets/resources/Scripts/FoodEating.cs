using UnityEngine;
using System.Collections;

public class FoodEating : MonoBehaviour {

    private GameObject big;
    private float speed = 2;
	// Use this for initialization
	void Start () {
        big = GameObject.Find("Big");
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, big.transform.position, speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Big")
        {
            big.transform.localScale += new Vector3(.1f, .1f, .1f);
            Destroy(gameObject);
        }
    }
}
