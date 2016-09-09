using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

    int size = 0;
    Color col;
    Vector3 initialScale = new Vector3(0.06f, 0.06f, 1f);
    Vector3 scaleUp = new Vector3(0.0005f, 0.0005f, 0.0f);

    // Use this for initialization
    void Start()
    {
        //initialScale = transform.localScale;
        col.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spRend = transform.GetComponent<SpriteRenderer>();
        col = spRend.color;

        col.a -= 0.02f;
        transform.localScale += scaleUp;
        size++;

        if (col.a <= 0)
            GameObject.Destroy(gameObject);

        spRend.color = col;
    }
}
