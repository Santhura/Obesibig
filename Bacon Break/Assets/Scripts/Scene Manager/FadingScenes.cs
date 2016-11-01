using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingScenes : MonoBehaviour {

    public Image fadeOutImage;               // the texture that will overlay the screen. This can be a black iamge or a loading graphic
    public float fadeSpeed = 3f;             // the fading speed

    private float alpha = 1.0f;              // the texture's alpha value between 0 and 1
    private float fadeDir = 1;               // the direction to fade : in = -1 or out = 1

    public static bool activateFade;        // Activefade when the fading has to start
    private Color fadeColor;                // color that will be set for the image

	// Use this for initialization
	void Start () {
        fadeOutImage = GameObject.Find("Fade").GetComponent<Image>();
        activateFade = false;
        fadeColor = new Color(0, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (activateFade) {
            fadeColor.a += fadeSpeed * fadeDir * Time.deltaTime;
            fadeOutImage.color = fadeColor;
            if(fadeColor.a >= 1) {
                GameManager.SwitchScene("Main Menu", null);
            }
        }
	}
}
