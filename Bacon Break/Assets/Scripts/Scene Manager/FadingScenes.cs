using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingScenes : MonoBehaviour {

    private GameObject fadeImage;           // create a new gameobject with a image that will fade in or out
    public float fadeSpeed = 1f;            // the fading speed
    public float fadeDir = 1;               // the direction to fade : in = -1 or out = 1

    public string sceneName;                // if switching scenes add the scene name
    public string levelName;

    public bool activateFade;               // Activefade when the fading has to start
    public Color fadeColor;                 // color that will be set for the image

    public GameObject FadeImage             // get and set for the fadeIamge
    {
        get { return fadeImage; }
        set { fadeImage = value; }
    }

    private GameObject loadingCanvas;       // a canvas that will exist through out the game
    public bool fadingIn = true;            // check if it is fading in


    // Use this for initialization
   protected void Awake () {
        if (Application.loadedLevelName == "StartGameScene") {
            loadingCanvas = GameObject.FindWithTag("Loading");
            DontDestroyOnLoad(loadingCanvas);
            fadeImage = new GameObject();
            fadeImage.name = "Fade";
            fadeImage.AddComponent<Image>();
            fadeImage.GetComponent<Image>().color = fadeColor;
            fadeImage.transform.SetParent(GameObject.Find("Fade objects").transform);
            fadeImage.transform.localScale = new Vector3(133, 133, 1);

            if (fadeColor.a == 1) {
                activateFade = true;
            }
            else
                activateFade = false;
        }
	}

        // Update is called once per frame
        protected void Update () {
            Fading(fadeDir, sceneName);
    }

    /// <summary>
    /// It will fade in or out depending on what the value of alpha is from the image.
    /// </summary>
    /// <param name="fadingDir"></param>
    /// <param name="sceneName"></param>
    public void Fading(float fadingDir, string sceneName) {
        if (activateFade) {
            if (fadeColor.a >= 1 && !fadingIn) {
                fadeDir = -1;
            }
            if (fadeDir == 1) {
                fadeImage.SetActive(true);
            }
                                 
            fadeColor.a += fadingDir * fadeSpeed * Time.deltaTime;
            fadeImage.GetComponent<Image>().color = fadeColor;

            if (fadeColor.a >= 1 && fadingDir == 1) {
                StartCoroutine("WaitForSwitch");
            }
            else if(fadeColor.a <= 0 && fadingDir == -1) {
                fadeImage.SetActive(false);
                activateFade = false;
            }
        }
    }
    IEnumerator WaitForSwitch() {
        yield return new WaitForSeconds(1);
        if (levelName == null) {
            GameManager.SwitchScene(sceneName, null);
        }
        else {
            GameManager.SwitchScene(sceneName, levelName);
        }
        fadingIn = false;
    }
}
