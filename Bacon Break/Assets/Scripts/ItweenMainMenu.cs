using UnityEngine;
using System.Collections;

public class ItweenMainMenu : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Menu Path"), "time", 15, "orienttopath", true, "Looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.easeInOutSine));
    }
}
