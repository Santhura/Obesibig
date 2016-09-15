using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaconScript : MonoBehaviour {

    public Text txt_baconAmount;    //UI element displaying the amount of bacon collected.
    public Image bar_stamina;       //UI element displaying the amount of stamina.

    private int baconAmount;        //To keep track of the amount of collected bacon in code.

	// Use this for initialization.
	void Start () {
        baconAmount = 0;
	}
	
	// Update is called once per frame.
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        //Add bacon points and stamina when the player collides with the bacon object.
        //Destroy the bacon object.
        if (col.gameObject.tag == "Player")
        {
            baconAmount++;
            bar_stamina.GetComponent<StaminaScript>().AddStamina();
            txt_baconAmount.text = baconAmount.ToString();

            Destroy(gameObject);
        }
    }
}
