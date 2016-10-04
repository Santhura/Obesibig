using UnityEngine;
using System.Collections;

public class ObjectMarking : MonoBehaviour {

    private float speedx, speedz, rotated = 0;
    private float speed = 1.0f;
    private bool triggerBridgeA = false;
    private float lerpTime = 1.0f;
    private float currentLerpTime = 0f;

    private Vector3 currentRotation = new Vector3(225, 0,180);
    private Vector3 destinationRotation = new Vector3(-40, 0, 180);

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

        //Sets the object rotation to its starting angle
        transform.eulerAngles = currentRotation;
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            tapped++;

            if (tapped == 3)
            {
                //activate object
                triggerBridgeA = true;
            }
        }

        //Rotate the bridge
        if (triggerBridgeA)
        {
            if (tapped == 3)
            {
                currentRotation = Vector3.Lerp(currentRotation, destinationRotation, Time.deltaTime * speed);

                transform.eulerAngles = currentRotation;
            }
                else
                {
                triggerBridgeA = false;
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
