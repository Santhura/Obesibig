using UnityEngine;
using System.Collections;

public class ObjectMarking : MonoBehaviour {

    private float speedx, speedz, rotated = 0;
    private bool triggerBridgeA = false;

    Renderer rend;
    bool confirmed, selected;
    private int tapped = 0;

    Transform childComponents;

    // Use this for initialization
    void Start()
    {
        speedx = -0.1f;
        confirmed = false;
        selected = false;
        rend = GetComponent<Renderer>();

        gameObject.transform.Rotate(new Vector3(-45.0f,transform.rotation.y,transform.rotation.z));
    }

    // Update is called once per frame
    void Update()
    {
        rotated = transform.rotation.x;
        Debug.Log("the rotation of the bridge is now at " + rotated);

        if (Input.GetMouseButtonUp(0) && confirmed)
        {
            tapped++;
            Debug.Log(tapped);
            if (tapped == 3)
            {
                //activate object
                triggerBridgeA = true;

                //reset tapped variable
                tapped = 0;  
            }
        }

        //Rotate the bridge
        if (triggerBridgeA)
        {
            if (tapped == 3)
            {
                //increase fall down speed
                speedx += 0.05f;
                gameObject.transform.Rotate(new Vector3(-45.0f * Time.deltaTime, 0.0f, speedz));
                Debug.Log("rotated the bridge");
            }
                else
                {
                    //Debug.Log("turned off bridge rotating");
                    //triggerBridgeA = false;
                    speedx = 0f;
                }
        }

    }

    void OnMouseEnter()
    {
        if (!confirmed)
        {
            rend.material.SetColor("_Color", Color.yellow);
        }
    }

    void OnMouseOver()
    {
        //confirms selecting by highlighting it yellow
        if (Input.GetMouseButtonDown(0) && !confirmed)
        {
            //highlight selected object with yellow
            rend.material.SetColor("_Color", Color.green);

            //do something with that the previouse step
        }

        if (Input.GetMouseButtonUp(0) && !confirmed)
        {
            confirmed = true;
        }

        if (Input.GetMouseButtonUp(0) && selected)
        {
            rend.material.SetColor("_Color", Color.white);
            selected = false;
            confirmed = false;
        }
    }

    void OnMouseExit()
    {
        if (confirmed)
        {
            //stays selected color yellow
            selected = true;
        }
        else {
            rend.material.SetColor("_Color", Color.white);
        }
    }
}
