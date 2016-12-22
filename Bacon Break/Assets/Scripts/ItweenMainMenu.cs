using UnityEngine;
using System.Collections;

public class ItweenMainMenu : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Menu Path"), "time", 15, "orienttopath", true, "Looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.easeInOutSine));
    }


    /// <summary>
    /// This method is only for the main menu quit button
    /// So I do not have to create a other script or 
    /// something else to make this work
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
