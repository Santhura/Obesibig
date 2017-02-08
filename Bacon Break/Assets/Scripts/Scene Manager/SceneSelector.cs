using UnityEngine;
using System.Collections;

public class SceneSelector : MonoBehaviour {

    FadingScenes fadingScenes;

    void Start()
    {
        fadingScenes = GameObject.FindWithTag("GameManager").GetComponent<FadingScenes>();
    }

    // Load the scene that has to be loaded
    public void SwitchScene(string sceneName) {
        Time.timeScale = 1;
        fadingScenes.fadeDir = 1;
        fadingScenes.fadeSpeed = 2;
        fadingScenes.sceneName = sceneName;
        fadingScenes.levelName = null;
        fadingScenes.fadingIn = true;
        fadingScenes.activateFade = true;
    }
}
