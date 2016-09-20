using UnityEngine;
using System.Collections;

public class BridgeOpener : MonoBehaviour {

    private float speedx, speedy, speedz = 0;
    private float angle = 00.0f;
    public bool triggerBridgeA = false;

    // Use this for initialization
    void Start () {
        speedx = -0.1f;
	}
	
	// Update is called once per frame
	void Update () {

        //DrawBridge closing.
        if (triggerBridgeA)
        {
            //increase fall down speed
            speedx += -0.05f;
            this.transform.Rotate(new Vector3(speedx, speedy, speedz));
            Debug.Log(this.gameObject.transform.localRotation.x);

            //stop rotating
            if (this.transform.localRotation.x <= -0.001)
            {  
                triggerBridgeA = false;
                speedx = -0.1f;
            }
        }
        
	}
}
