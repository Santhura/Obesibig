using UnityEngine;
using System.Collections;

public class FloatingDebris : MonoBehaviour {

    private Vector3 destinationPosition = new Vector3(0, -1, 0);
    public float time = 1.5f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        iTween.MoveAdd(gameObject, iTween.Hash("amount", destinationPosition, "time", time, "easytype", iTween.EaseType.easeInQuad, "looptype", iTween.LoopType.pingPong));
    }
}
