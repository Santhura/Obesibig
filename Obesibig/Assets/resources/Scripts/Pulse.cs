using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

    int size = 0;
    Vector3 initialScale = new Vector3(0.06f, 0.06f, 1f);
    Vector3 scaleUp = new Vector3(0.0005f,0.0005f,0.0f);

    // Use this for initialization
    void Start () {
        //initialScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer spRend = transform.GetComponent<SpriteRenderer>();
        Color col = spRend.color;
        if (size <= 20)
        {
            col.a -= 0.011f;
            transform.localScale += scaleUp;
            size++;
        }
        else if (size>20 &&size<100)
        {
            col.a -= 0.011f;
            transform.localScale -= (scaleUp*2);
            size++;
        }
        else
        {
            col.a = 1;
            transform.localScale = initialScale;
            size = 0;
        }

        spRend.color = col;
    }
}
