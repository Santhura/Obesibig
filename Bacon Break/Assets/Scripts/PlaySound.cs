using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
    public AudioClip pickupSound;
    AudioSource canvasSource;
    // Use this for initialization
    void Start () {
        canvasSource = GameObject.Find("UI_Canvas").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canvasSource.clip = pickupSound;
            canvasSource.Play();
        }
    }
}
