using UnityEngine;
using System.Collections;

public class SceneSelector : MonoBehaviour {

    // Load the scene that has to be loaded
    public void SwitchScene(string sceneName) {
        UIManager.SwitchScene(sceneName);
    }
}
