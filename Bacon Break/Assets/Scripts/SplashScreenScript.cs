using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {
    
	// Use this for initialization
    //Shows splash screen for couple seconds
	IEnumerator Start () {
        FadingScenes fadingScenes = GameObject.FindWithTag("GameManager").GetComponent<FadingScenes>();
        yield return new WaitForSeconds(2);
        fadingScenes.sceneName = "Main Menu";
        fadingScenes.activateFade = true;
    }
}
