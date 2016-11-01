using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {
    
	// Use this for initialization
    //Shows splash screen for couple seconds
	IEnumerator Start () {
        yield return new WaitForSeconds(2);
        GameManager.SwitchScene("Main Menu", null);
    }
}
