using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour {
    public float estimatedSpeed;
    public static bool isBoosting;
    public GameObject stanimaParticles;
    private float decreaseParticlesPos = 65;
    private const float maxWidthOfStanimabar = 330;
    private const float min = -175;
    private const float max = 155;

    // Use this for initialization
    void Start() {
        stanimaParticles = GameObject.Find("Stanima Particle");
        isBoosting = false;
        gameObject.GetComponent<Image>().fillAmount /= 2.5f;
        float foo = maxWidthOfStanimabar / 5 * 2;
        float beginXPos = maxWidthOfStanimabar - foo;
        stanimaParticles.transform.localPosition = new Vector3(Mathf.Clamp(stanimaParticles.transform.localPosition.x - beginXPos, min, max), stanimaParticles.transform.localPosition.y, stanimaParticles.transform.localPosition.z);
        stanimaParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //Stamina decreases over time.
        DrainStamina();
        estimatedSpeed = gameObject.GetComponent<Image>().fillAmount;
    }

    private void DrainStamina() {
        //Decrease fillAmount to simulate UI stamina drain.
        if (isBoosting && gameObject.GetComponent<Image>().fillAmount > 0) {
            stanimaParticles.SetActive(true);
            gameObject.GetComponent<Image>().fillAmount -= Time.deltaTime / 5;
            stanimaParticles.transform.localPosition = new Vector3(stanimaParticles.transform.localPosition.x - decreaseParticlesPos * Time.deltaTime,
                                                                    stanimaParticles.transform.localPosition.y, stanimaParticles.transform.localPosition.z);
        }
        else {
            isBoosting = false;
            stanimaParticles.SetActive(false);
        }
    }

    public void AddStamina() {
        //Increase fillAmount to simulate UI stamina gain.
        //(By collecting bacon objects, see "BaconScript").
        gameObject.GetComponent<Image>().fillAmount += 0.2f;
        float newXPos = maxWidthOfStanimabar / 5 * 1;
        stanimaParticles.transform.localPosition = new Vector3(Mathf.Clamp(stanimaParticles.transform.localPosition.x + newXPos, min, max), stanimaParticles.transform.localPosition.y, stanimaParticles.transform.localPosition.z);


    }

    public void BoostButton() {
        if (gameObject.GetComponent<Image>().fillAmount > 0) {
            if (isBoosting)
                isBoosting = false;
            else
                isBoosting = true;
        }
    }
}
