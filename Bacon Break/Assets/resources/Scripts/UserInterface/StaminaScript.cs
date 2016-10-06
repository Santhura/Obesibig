using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Stamina decreases over time.
        DrainStamina();
	}

    private void DrainStamina()
    {
        //Decrease fillAmount to simulate UI stamina drain.
        gameObject.GetComponent<Image>().fillAmount -= Time.deltaTime / 50;
    }

    public void AddStamina()
    {
        //Increase fillAmount to simulate UI stamina gain.
        //(By collecting bacon objects, see "BaconScript").
        gameObject.GetComponent<Image>().fillAmount += 0.2f;
    }
}
