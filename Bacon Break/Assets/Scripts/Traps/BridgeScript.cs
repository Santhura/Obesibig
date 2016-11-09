using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour
{
    private float time = 0.2f;
    private bool bridgeIsDown = false;
    private RaycastHit hit;
    private AudioSource tapAudio;

    private Vector3 startRotation = new Vector3(-45, 0, 0);
    private Vector3 destinationRotation = new Vector3(15, 0, 0);

    private int tapped = 0;


    // Use this for initialization
    void Start()
    {
        transform.Rotate(startRotation);
        tapAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // get touch position and check if the right object is hit to move it.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.GetInstanceID() == transform.gameObject.GetInstanceID())
            {
                if (Input.GetMouseButtonUp(0) && !bridgeIsDown && (tapped <= 3))
                {
                    RotateTheBridge();
                    tapAudio.Play();
                }
                else if (bridgeIsDown)
                    transform.tag = "Untagged";
            }
        }
    }

    void RotateTheBridge()
    {
        iTween.RotateAdd(gameObject, iTween.Hash("amount", destinationRotation, "time", time, "easytype", iTween.EaseType.easeOutCubic, "oncomplete", "TappedAmount"));
    }

    //Stops rotating the bridge when in the correct rotational position
    void TappedAmount()
    {
        tapped++;
        if (tapped == 3)
        {
            bridgeIsDown = true;
        }
    }

}
