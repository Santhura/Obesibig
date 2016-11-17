using UnityEngine;
using System.Collections;

public class ArrowHelperScript : MonoBehaviour
{

    public float wobbleFactor;

    // Use this for initialization
    void Start()
    {
        iTween.FadeTo(GameObject.Find("ArrowSprite1"), iTween.Hash("alpha", 0f, "time", 0f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //Add bacon points and stamina when the player collides with the bacon object.
        //Destroy the bacon object.
        if (col.gameObject.tag == "Player")
        {
            iTween.FadeTo(GameObject.Find("ArrowSprite1"), iTween.Hash("alpha", 1f, "time", 0.5f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none));
            iTween.MoveAdd(GameObject.Find("ArrowSprite1"), iTween.Hash("amount", new Vector3(8f, 0, 1f), "time", 0.25f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
            iTween.PunchScale(GameObject.Find("ArrowSprite1"), iTween.Hash("amount", new Vector3(wobbleFactor, wobbleFactor, 0), "time", 1f, "easetype", iTween.EaseType.easeInCubic, "looptype", iTween.LoopType.pingPong));
        }
    }

}
