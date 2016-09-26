using UnityEngine;
using System.Collections;

public class ObjectMarking : MonoBehaviour {

    private float speedx, speedy, speedz = 0;
    private float angle = 00.0f;
    private bool triggerBridgeA = false;

    Renderer rend;
    bool confirmed, selected;
    private int tapped = 0;

    private string selection;
    BridgeOpener bridgeOpenerScript;
    BridgeOpener bOS;
    Transform childComponents;

    // Use this for initialization
    void Start()
    {
        //GameObject bridgeObject = GameObject.Find("RotatorBridge");
        
        //childComponents = gameObject.GetComponentInParent<Transform>();
        //bridgeOpenerScript = childComponents.transform.GetComponent<BridgeOpener>();

        speedx = -0.1f;
        selection = "Nothing Selected yet";
        confirmed = false;
        selected = false;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    
        Debug.Log("Tapped " + tapped);
        if (Input.GetMouseButtonUp(0) && confirmed)
        {
            tapped++;

            if (tapped == 3)
            {
                //activate object
                //bridgeOpenerScript.triggerBridgeA = true;

                //bOS = childComponents.gameObject.GetComponent<BridgeOpener>();
                //bOS.triggerBridgeA = true;
                triggerBridgeA = true;

                //reset tapped variable
                tapped = 0;  
            }
        }

        //Rotate the bridge
        //DrawBridge closing.
        if (triggerBridgeA)
        { 
            //stop rotating
            if (transform.rotation.x > 0)
            {
                //increase fall down speed
                speedx += 0.05f;
                gameObject.transform.Rotate(new Vector3(speedx, speedy, speedz));
            }
            else
            {
                Debug.Log("asdf");
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
            //ConfirmSelection();
            //tapped++;

            //highlight selected object with yellow
            rend.material.SetColor("_Color", Color.green);

            // get name back what card it is.
            selection = gameObject.name;
            //print(selection);

            //do something with that the previouse step

            
            //reset selection variable.
            selection = "Nothing selected";
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
