using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    // transform of the camrea to shake. Grabs the gameObject's transform
    private Transform camTransform;

    //how long the object should shake for.
    public float shakeDuration = 0;

    // amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    // orgiginal postiion of the camera
    private Vector3 originalPos;
    // check if the player is hit by any danger.
    public static bool isHit;


    public static GameObject deathParticle;         // show the death particle system

    void Awake() {
   //     deathParticle = GameObject.Find("Pig_death_Particle");
    //    deathParticle.SetActive(false);
        isHit = false;
        if (camTransform == null) {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }


    // Update is called once per frame
    void Update() {
        if (isHit) {
            Shake();
        }
    }

    /// <summary>
    /// Shake the camera for some amount of time.
    /// When finished set camera position to original position
    /// </summary>
    private void Shake() {
        if (shakeDuration > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0.3f;
            camTransform.localPosition = originalPos;
            isHit = false;
        }
    }
}
