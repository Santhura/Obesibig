using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {

    public Button[] disableMMbuttons;
    private GameObject splashPanel;

    private static bool splashScreenEnabled;

    void Awake () {
        splashPanel = GameObject.Find("SplashPanel");
        if (!splashScreenEnabled) {
            for (int i = 0; i < disableMMbuttons.Length; i++) {
                disableMMbuttons[i].interactable = false;
            }
        }
        else {
            for (int i = 0; i < disableMMbuttons.Length; i++) {
                disableMMbuttons[i].interactable = true;
            }
            splashPanel.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
        splashScreenEnabled = false;

    }

    // Update is called once per frame
    void Update () {
        if (!splashScreenEnabled) {
            StartCoroutine(Waitforseconds());


            if (Input.GetTouch(0).phase == TouchPhase.Began) {

                splashPanel.SetActive(false);

                for (int i = 0; i < disableMMbuttons.Length; i++) {
                    disableMMbuttons[i].interactable = true;
                }
                splashScreenEnabled = true;

                GetComponent<SplashScreenScript>().enabled = false;
            }
            splashScreenEnabled = true;
        }
	}

    IEnumerator Waitforseconds() {

        yield return new WaitForSeconds(3);
        splashPanel.SetActive(false);

        for (int i = 0; i < disableMMbuttons.Length; i++)
        {
            disableMMbuttons[i].interactable = true;
        }
        splashScreenEnabled = true;
        GetComponent<SplashScreenScript>().enabled = false;
    }
}
