using UnityEngine;
using System.Collections;

public class StaminaParticleMovement : MonoBehaviour {

    private readonly string playerTag = "Player";
    private Transform targetPosition;
    private float speed = 80;
   
	// Use this for initialization
	void Awake () {
        gameObject.transform.position = GameObject.FindWithTag(playerTag).transform.position;
        targetPosition = GameObject.Find("TargetStamina").transform;
       // Destroy(gameObject, 1.5f);

    }
	
	// Update is called once per frame
	void Update () {
        if(targetPosition != null)
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.position, speed * Time.deltaTime);

        if (targetPosition != null) {
            if (Vector3.Distance(gameObject.transform.position, targetPosition.position) < 1) {
                GetComponent<ParticleSystem>().enableEmission = false;
                Destroy(gameObject, .5f);
            }
        }
	}
}
