using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour
{
    private float time = 0.2f;
    private RaycastHit hit;
    private AudioSource tapAudio;

    public Vector3 startRotation = new Vector3(-45, 0, 0);
    private Vector3 destinationRotation = new Vector3(15, 0, 0);

    private int tapped = 0;

    public bool activated;

    public bool isTutorial; //check if this bridge is used as a tutorial
    public GameObject tutorialIcon; //if it is a tutorial, we can use this to call upon the tutorial icon.


    // Use this for initialization
    void Start()
    {
        activated = false;

        transform.Rotate(startRotation);
        tapAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Tapped()
    {
        RotateTheBridge();
        tapAudio.Play();
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
            Destroy(tutorialIcon);
            activated = true;
        }
    }

}
