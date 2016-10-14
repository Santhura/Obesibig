using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {

    public Button[] disableMMbuttons;
    private GameObject splashPanel;

    void Awake () {

        splashPanel = GameObject.Find("SplashPanel");

        for (int i = 0; i < disableMMbuttons.Length; i++)
        {
            disableMMbuttons[i].interactable = false;
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(Waitforseconds());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Waitforseconds() {

        yield return new WaitForSeconds(3);
        splashPanel.SetActive(false);

        for (int i = 0; i < disableMMbuttons.Length; i++)
        {
            disableMMbuttons[i].interactable = true;
        }


    }
}
