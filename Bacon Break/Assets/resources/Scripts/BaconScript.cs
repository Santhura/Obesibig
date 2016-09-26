using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaconScript : MonoBehaviour {

    public GameObject pnl_score;           //UI element for displaying the total score (of bacon).
    public GameObject bar_stamina;         //UI element displaying the amount of stamina.

	// Use this for initialization.
	void Start () 
    {
        pnl_score = GameObject.Find("pnl_score");
        bar_stamina = GameObject.Find("bar_stamina");

    }
	
	// Update is called once per frame.
	void Update ()
    {
	}

    void OnCollisionEnter(Collision col)
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
