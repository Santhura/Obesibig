using UnityEngine;
using System.Collections;

public class ItweenMainMenu : MonoBehaviour {

    private GameObject player;
    private int startIndex, endIndex;
    private Vector3[] subPath;
    public Vector3[] nodes;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");

        startIndex = 0;
        endIndex = nodes.Length;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3[] path = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];
        path = GetPath(startIndex, endIndex);
        iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 5, "orienttopath", true, "easetype", iTween.EaseType.easeInOutSine));
    }

    Vector3[] GetPath(int startIndex, int endIndex) {
        subPath = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];

        //Move path in opposite direction if startIndex is greater than endIndex.
        //Negative or positive value...
        int sign;
        if (startIndex > endIndex) {
            sign = -1;
        }
        else {
            sign = 1;
        }

        //Assign requested values from nodes to path.
        for (int i = 0; i < subPath.Length; i++) {
            subPath[i] = nodes[startIndex + (sign * i)];
        }

        return subPath;
    }
}
