using UnityEngine;
using System.Collections;

public class SceneSelector : MonoBehaviour {

    FadingScenes fadingScenes;

    void Start()
    {
        fadingScenes = GameObject.FindWithTag("Canvas").GetComponent<FadingScenes>();
    }

    // Load the scene that has to be loaded
    public void SwitchScene(string sceneName) {
        Time.timeScale = 1;
        fadingScenes.fadeDir = 1;
        fadingScenes.sceneName = sceneName;
        fadingScenes.levelName = null;
        fadingScenes.FadeImage.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        fadingScenes.FadeImage.transform.position = GameObject.FindWithTag("Canvas").transform.position;
        fadingScenes.FadeImage.transform.rotation = GameObject.FindWithTag("Canvas").transform.rotation;
        FadingScenes.activateFade = true;
    }

    public void SwitchLevel(string sceneName/*, string levelName*/) {
        Time.timeScale = 1;
      //  fadingScenes.levelName = levelName;
        fadingScenes.fadeDir = 1;
        fadingScenes.sceneName = sceneName;
        fadingScenes.FadeImage.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        fadingScenes.FadeImage.transform.position = GameObject.FindWithTag("Canvas").transform.position;
        fadingScenes.FadeImage.transform.rotation = GameObject.FindWithTag("Canvas").transform.rotation;
        FadingScenes.activateFade = true;
    }
}
