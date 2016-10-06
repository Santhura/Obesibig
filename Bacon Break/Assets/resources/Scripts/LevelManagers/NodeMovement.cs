using UnityEngine;
using System.Collections;

/*This class is used for moving a character to a levelnode.
 * When the player taps a node, the character will move to it from the node it's currently positioned at.
 * TODO:: Popup with play button, level prefab added to node so it can be loaded.
 */

public class NodeMovement : MonoBehaviour
{
    //Pubs
    public Vector3[] nodes;                     //Masterlist of iTween nodes

    //Privates ( ;) )
    private Vector3[] subPath;                  //Sub-path coordinates, used for character movement between level nodes
    private GameObject player;                  //The player, obviously
    private int startIndex, endIndex;           //The start index and the end index for the sub path array
    private bool isMoving;                      //Boolean which checks if the character is moving from one node to another

    // Use this for initialization
    void Start()
    {
        //Start index of the iTween path.
        startIndex = 0;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame.
    void Update()
    {
        //FOR COMPUTER DEBUG PURPOSES, DO NOT REMOVE.
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(startIndex);
            endIndex = 2;

            Vector3[] path = new Vector3[50];
            path = GetPath(0, 2);

            iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 10, "easetype", iTween.EaseType.easeInOutSine));
            isMoving = true;
        }*/

        //Move the player to the requested location.
        for (int i = 0; i < Input.touchCount; ++i)
        {
            //Can only start movement when the character isn't already moving.
            if (Input.GetTouch(i).phase == TouchPhase.Began && !isMoving)
            {
                //Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;

                //Check if node is tapped and move the character to that node.
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Node")
                    {
                        //Use start and end index to get a sub-path the character can traverse.
                        GameObject levelNode = hit.transform.gameObject;
                        endIndex = int.Parse(levelNode.name.Substring(levelNode.name.Length - 2)) * 2;

                        Vector3[] path = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];
                        path = GetPath(startIndex, endIndex);
                        
                        //Move the object to the specified location using the sub-path.
                        iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 10, "easetype", iTween.EaseType.easeInOutSine));

                        //Character is on the move.
                        isMoving = true;
                    }
                }
            }

            //Check if the character has reached the specified node.
            if (isMoving)
            {
                if (Mathf.Abs(Vector3.Distance(player.transform.position, nodes[endIndex])) <= 0.5f)
                {
                    startIndex = endIndex;
                    isMoving = false;
                }
            }
        }
    }

    Vector3[] GetPath(int startIndex, int endIndex)
    {
        subPath = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];

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

        //Assign requested values from nodes to path.
        for (int i = 0; i < subPath.Length; i++)
        {
            subPath[i] = nodes[startIndex + (sign * i)];
        }

        return subPath;
    }
}
