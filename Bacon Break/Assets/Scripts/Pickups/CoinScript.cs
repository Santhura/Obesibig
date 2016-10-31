using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour {

    public GameObject pnl_score;   //UI element for displaying the total score (of bacon).

    // Use this for initialization.
    void Start () 
    {
        pnl_score = GameObject.Find("pnl_score");
        //   bar_stamina = GameObject.Find("bar_stamina");

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
            pnl_score.GetComponent<ScoreScript>().AddCoin();

            Destroy(gameObject);
        }
    }
}
