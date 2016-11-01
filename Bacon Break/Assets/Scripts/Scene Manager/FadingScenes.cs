using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingScenes : MonoBehaviour {

    public Image fadeOutImage;               // the texture that will overlay the screen. This can be a black iamge or a loading graphic
    public float fadeSpeed = 3f;             // the fading speed

    public float fadeDir = 1;               // the direction to fade : in = -1 or out = 1
    public string sceneName;

    public static bool activateFade;        // Activefade when the fading has to start
    public Color fadeColor;                // color that will be set for the image

	// Use this for initialization
	void Start () {
        fadeOutImage = GameObject.Find("Fade").GetComponent<Image>();
        activateFade = false;
	}
	
	// Update is called once per frame
	void Update () {
        Fading(fadeDir, sceneName);
    }

    private void Fading(float fadingDir, string sceneName) {
        if (activateFade) {
            fadeColor.a += fadeSpeed * fadingDir * Time.deltaTime;
            fadeOutImage.color = fadeColor;
            if (fadeColor.a >= 1) {
                GameManager.SwitchScene(sceneName, null);
            }
        }
    }
}
