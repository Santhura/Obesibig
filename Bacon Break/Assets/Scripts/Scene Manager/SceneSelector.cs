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
        //GameManager.SwitchScene(sceneName, null);
        Debug.Log(sceneName);
        FadingScenes.activateFade = true;
        fadingScenes.fadeDir = 1;
        fadingScenes.sceneName = sceneName;
        fadingScenes.FadeImage.transform.parent = GameObject.FindWithTag("Canvas").transform;
        fadingScenes.Fading(fadingScenes.fadeDir, fadingScenes.sceneName);
    }
}
