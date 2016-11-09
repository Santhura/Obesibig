using UnityEngine;
using System.Collections;

public class CameraRepositionScript : MonoBehaviour {

    public bool tunnelLv8 = false;
    private Transform cameraOldtransform;
    private Transform cameraNewtransform;
    public GameObject getCamera;

	// Use this for initialization
	void Start () {
        getCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
        if (tunnelLv8)
        {
            cameraNewtransform = GameObject.Find("TunnelPosition").transform;

            iTween.MoveTo(getCamera, iTween.Hash("position", cameraNewtransform.position, "time", 1f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            //getCamera.transform.position = cameraNewtransform.position;
            iTween.RotateTo(getCamera, iTween.Hash("x", 30f, "time", 1f, "islocal", true, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            //getCamera.transform.rotation = cameraNewtransform.rotation;

            tunnelLv8 = false;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(gameObject.name == "ArrowTrigger1")
        { 
            tunnelLv8 = true;
        }

        if (gameObject.name == "TriggerCamereOldPos")
        {
            Debug.Log("Old CAMERA Trigger");
            cameraOldtransform = GameObject.Find("StandardCameraPosition").transform;
            iTween.MoveTo(getCamera, iTween.Hash("position", cameraOldtransform.position, "time", 1f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            //getCamera.transform.position = cameraOldtransform.position;
            iTween.RotateTo(getCamera, iTween.Hash("x", 35.4f, "time", 1f, "islocal", true, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            //getCamera.transform.rotation = cameraOldtransform.rotation;
        }
    }


}
