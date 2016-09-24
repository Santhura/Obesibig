using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaconScript : MonoBehaviour {

    public Image pnl_score;           //UI element for displaying the total score (of bacon).
    public Image bar_stamina;         //UI element displaying the amount of stamina.

	// Use this for initialization.
	void Start () 
    {
	}
	
	// Update is called once per frame.
	void Update ()
    {
	}

    void OnTriggerEnter(Collider col)
    {
        //Add bacon points and stamina when the player collides with the bacon object.
        //Destroy the bacon object.
        if (col.gameObject.tag == "Player")
        {
            bar_stamina.GetComponent<StaminaScript>().AddStamina();
            pnl_score.GetComponent<ScoreScript>().AddBacon();

            Destroy(gameObject);
        }
    }
}
