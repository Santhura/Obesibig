using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour {
    public float estimatedSpeed;
	// Use this for initialization
	void Start () 
    {
        gameObject.GetComponent<Image>().fillAmount /= 1.75f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Stamina decreases over time.
        DrainStamina();
        estimatedSpeed = gameObject.GetComponent<Image>().fillAmount;
    }

    private void DrainStamina()
    {
        //Decrease fillAmount to simulate UI stamina drain.
        gameObject.GetComponent<Image>().fillAmount -= Time.deltaTime /20;
    }

    public void AddStamina()
    {
        //Increase fillAmount to simulate UI stamina gain.
        //(By collecting bacon objects, see "BaconScript").
        gameObject.GetComponent<Image>().fillAmount += 0.20f;
    }
}
