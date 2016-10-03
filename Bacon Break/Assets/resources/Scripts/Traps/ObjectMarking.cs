using UnityEngine;
using System.Collections;

public class ObjectMarking : MonoBehaviour {

    private float speedx, speedz = 0;
    private bool triggerBridgeA = false;

    Renderer rend;
    bool confirmed, selected;
    private int tapped = 0;

    Transform childComponents;

    // Use this for initialization
    void Start()
    {
        //GameObject bridgeObject = GameObject.Find("RotatorBridge");
        
        //childComponents = gameObject.GetComponentInParent<Transform>();
        //bridgeOpenerScript = childComponents.transform.GetComponent<BridgeOpener>();

        speedx = -0.1f;
        confirmed = false;
        selected = false;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonUp(0) && confirmed)
        {
            tapped++;

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
            if (transform.rotation.x > 0)
            {
                //increase fall down speed
                speedx += 0.05f;
                gameObject.transform.Rotate(new Vector3(speedx, 0.0f, speedz));
            }
            else
            {
                triggerBridgeA = false;
                speedx = 0f;
            }
        }

    }

    /*
    void OnMouseDown()
    {
        tapped++;
        if (tapped > 3)
            tapped = 0;
        else if (tapped == 3)
        {
            //open bridge here or set this.bridgeOpenerScript.triggerBridgeA = true;
            //this.bridgeOpenerScript.triggerBridgeA = true;

            bOS = childComponents.gameObject.GetComponent<BridgeOpener>();
            bOS.triggerBridgeA = true;
        }
    }*/

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
