using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingScenes : MonoBehaviour {

    private GameObject fadeImage;             // create a new gameobject with a image that will fade in or out
    public float fadeSpeed = 1f;             // the fading speed

    public float fadeDir = 1;               // the direction to fade : in = -1 or out = 1
    public string sceneName;                // if switching scenes add the scene name
    public string levelName;

    public static bool activateFade;        // Activefade when the fading has to start
    public Color fadeColor;                // color that will be set for the image

    public GameObject FadeImage
    {
        get { return fadeImage; }
        set { fadeImage = value; }
    }

	// Use this for initialization
	void Start () {

        fadeImage = new GameObject();
        fadeImage.name = "Fade";
        fadeImage.AddComponent<Image>();
        fadeImage.GetComponent<Image>().color = fadeColor;

        if (Application.loadedLevelName != "TutorialScene")
        {
            fadeImage.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        }

        fadeImage.transform.localScale = new Vector3(133, 133, 1);
        if (fadeColor.a == 1)
            activateFade = true;
        else
            activateFade = false;
	}
	
	// Update is called once per frame
	void Update () {
            Fading(fadeDir, sceneName, levelName);
    }

    public void Fading(float fadingDir, string sceneName, string levelName) {
        if (activateFade) {
            Debug.Log(levelName);

            Time.timeScale = 1;
            fadeColor.a += fadingDir * fadeSpeed * Time.deltaTime;
            fadeImage.GetComponent<Image>().color = fadeColor;
            if (fadeColor.a >= 1 && fadingDir == 1) {
                if (levelName == null) {
                    GameManager.SwitchScene(sceneName, null);
                }
                else  {
                    Debug.Log(levelName);
                    GameManager.SwitchScene(sceneName, levelName);
                }
            }
            else if(fadeColor.a <= 0 && fadingDir == -1) {
                fadeImage.transform.SetParent(null);
                activateFade = false;
            }
        }
    }
}
