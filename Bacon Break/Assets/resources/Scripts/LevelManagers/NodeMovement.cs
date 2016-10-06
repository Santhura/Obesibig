using UnityEngine;
using System.Collections;

public class NodeMovement : MonoBehaviour
{
    public Vector3[] nodes;
    private Vector3[] paths;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Vector3[] path = new Vector3[2];
            path = GetPath(0, 2);

            iTween.MoveTo(gameObject, iTween.Hash("position", path[path.Length-1], "time", 10, "easetype", iTween.EaseType.easeInOutSine));
        }
    }

    Vector3[] GetPath(int startIndex, int endIndex)
    {
        paths = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];

        //Move path in opposite direction if startIndex is greater than endIndex.
        //Negative or positive value...
        int sign;
        if (startIndex > endIndex)
        {
            sign = -1;
        }
        else
        {
            sign = 1;
        }

        //Assign values from nodes to path
        for (int i = 0; i < paths.Length; i++)
        {
            Debug.Log("Hoi");
            paths[i] = nodes[startIndex + (sign * i)];
            //paths[i] = nodes[i];
        }

        return paths;
    }
}
