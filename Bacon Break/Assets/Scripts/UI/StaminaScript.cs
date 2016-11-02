using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour {
    public float estimatedSpeed;
    public static bool isBoosting;

    // Use this for initialization
    void Start() {
        isBoosting = false;
        gameObject.GetComponent<Image>().fillAmount /= 2.5f;
    }

    // Update is called once per frame
    void Update() {
        //Stamina decreases over time.
        DrainStamina();
        estimatedSpeed = gameObject.GetComponent<Image>().fillAmount;
    }

    private void DrainStamina() {
        //Decrease fillAmount to simulate UI stamina drain.
        if (isBoosting && gameObject.GetComponent<Image>().fillAmount > 0)
            gameObject.GetComponent<Image>().fillAmount -= Time.deltaTime / 5;
        else
            isBoosting = false;
    }

    public void AddStamina() {
        //Increase fillAmount to simulate UI stamina gain.
        //(By collecting bacon objects, see "BaconScript").
        gameObject.GetComponent<Image>().fillAmount += 0.22f;
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
