using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingScenes : MonoBehaviour {

    private GameObject fadeImage;             // create a new gameobject with a image that will fade in or out
   // public Image fadeOutImage;               // the texture that will overlay the screen. This can be a black iamge or a loading graphic
    public float fadeSpeed = 3f;             // the fading speed

    public float fadeDir = 1;               // the direction to fade : in = -1 or out = 1
    public string sceneName;

    public static bool activateFade;        // Activefade when the fading has to start
    public Color fadeColor;                // color that will be set for the image

	// Use this for initialization
	void Start () {

        fadeImage = new GameObject();
        fadeImage.name = "Fade";
        fadeImage.AddComponent<Image>();
        fadeImage.GetComponent<Image>().color = fadeColor;
        fadeImage.transform.SetParent( GameObject.FindWithTag("Canvas").transform);
        fadeImage.transform.localScale = new Vector3(133, 133, 1);
        if (fadeColor.a == 1)
            activateFade = true;
        else
            activateFade = false;
	}
	
	// Update is called once per frame
	void Update () {
        Fading(fadeDir, sceneName);
    }

    private void Fading(float fadingDir, string sceneName) {
        if (activateFade) {
            fadeColor.a += fadingDir * fadeSpeed * Time.deltaTime;
            fadeImage.GetComponent<Image>().color = fadeColor;
            if (fadeColor.a >= 1 && fadingDir == 1) {
                GameManager.SwitchScene(sceneName, null);
            }
            else if(fadeColor.a <= 0 && fadingDir == -1) {

            }
        }
    }
}
