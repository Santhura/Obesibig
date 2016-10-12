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
    public GameObject pnl_level;                //Set active/inactive
    public GameObject pnl_refocus;              //If the player drags the screen too far away from the player, the option to refocus appears.

    //Privates ( ;) )
    private Vector3[] subPath;                  //Sub-path coordinates, used for character movement between level nodes
    private GameObject player;                  //The player, obviously
    private int startIndex, endIndex;           //The start index and the end index for the sub path array
    private bool isMoving;                      //Boolean which checks if the character is moving from one node to another
    private GameObject levelNode;               //For accessing the node of the level (for position purposes)
    private GameObject levelPrefab;             //For setting the MyLevel object (for level loading)
    private string levelName;                   //The name of the level.
    private int i;

    void Awake()
    {
        i = 0;
    }

    // Use this for initialization
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        player = GameObject.FindWithTag("Player");
        isMoving = false;

        //Activate level information panel of the current node the player is standing on
        if (!PlayerPrefs.HasKey("LevelIndex") && Camera.main.isActiveAndEnabled)
        {
            //Start index of the iTween path.
            startIndex = 0;

            levelNode = GameObject.Find("Level 0");
            levelPrefab = levelNode.GetComponent<LevelPrefab>().levelPrefab;
            levelName = levelNode.GetComponent<LevelPrefab>().levelPrefab.name;
            gameObject.GetComponent<LevelInfo>().SetLevelInformation(levelNode.transform.position, levelName, levelPrefab, 0);

            //Set player pos
            player.transform.position = levelNode.transform.position;
        }
        else
        {
            //Start index of the iTween path.
            startIndex = PlayerPrefs.GetInt("LevelIndex") * 2;

            levelNode = GameObject.Find("Level " + PlayerPrefs.GetInt("LevelIndex"));
            levelPrefab = levelNode.GetComponent<LevelPrefab>().levelPrefab;
            levelName = levelNode.GetComponent<LevelPrefab>().levelPrefab.name;
            gameObject.GetComponent<LevelInfo>().SetLevelInformation(levelNode.transform.position, levelName, levelPrefab, PlayerPrefs.GetInt("LevelIndex"));

            //Set player pos
            player.transform.position = levelNode.transform.position;
        }
    }

    // Update is called once per frame.
    void Update()
    {
        //If the node the pig is standing on is no longer visible,
        //Activate the refocus panel and deactivate the level information panel.
        if (isNodeVisible(PlayerPrefs.GetInt("LevelIndex")))
        {
            pnl_level.SetActive(true);
            pnl_refocus.SetActive(false);
        }
        else
        {
            pnl_level.SetActive(false);
            if (i > 2)
            {
                pnl_refocus.SetActive(true);
            }
        }

        i++;
        if (i > 2)
        {
            i = 3;
        }

        //Use the mouse in the editor, use swipe in build.
        #if UNITY_EDITOR
             Controls("Mouse");
        #else
             Controls("Swipe");
        #endif

        //Check if the character has reached the specified node.
        if (isMoving)
        {
            //Focus on piggy when it walks out of the camera view.
            if (!player.GetComponentInChildren<Renderer>().isVisible)
            {
                Camera.main.GetComponent<CameraDrag>().RefocusCamera();
            }

            pnl_level.SetActive(false);

            if (Mathf.Abs(Vector3.Distance(player.transform.position, nodes[endIndex])) <= 0.5f)
            {
                startIndex = endIndex;
                gameObject.GetComponent<LevelInfo>().SetLevelInformation(levelNode.transform.position, levelName, levelPrefab, endIndex / 2);
                isMoving = false;
            }
        }
        else
        {
            //Force the level information panel's position to be the same as the node position;
            //(If this isn't done, the panel will move with you whenever you drag the map around).
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, levelNode.transform.position);
            pnl_level.transform.position = screenPoint;

            GameObject.Find("Game Manager").GetComponent<LevelInfo>().ClampPanel();
        }
    }

    //Move the player to the requested location.
    void Controls(string controlType)
    {
        if (controlType == "Mouse")
        {
            if (Input.GetMouseButtonUp(0) && !isMoving)
            {
                //Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //Check if node is tapped and move the character to that node.
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Node")
                    {
                        if (PlayerPrefs.HasKey(hit.collider.name + "_unlocked"))
                        {
                            levelNode = hit.transform.gameObject;
                            levelPrefab = levelNode.GetComponent<LevelPrefab>().levelPrefab;
                            levelName = levelNode.GetComponent<LevelPrefab>().levelPrefab.name;

                            endIndex = int.Parse(levelNode.name.Substring(levelNode.name.Length - 2)) * 2;

                            Vector3[] path = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];
                            path = GetPath(startIndex, endIndex);

                            //Move the object to the specified location using the sub-path.
                            iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 5, "orienttopath", true, "easetype", iTween.EaseType.easeInOutSine));

                            //Character is on the move.
                            isMoving = true;
                        }
                    }
                }
            }
        }
        else if (controlType == "Swipe")
        {
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
                            if (PlayerPrefs.HasKey(hit.collider.name + "_unlocked"))
                            {
                                levelNode = hit.transform.gameObject;
                                levelPrefab = levelNode.GetComponent<LevelPrefab>().levelPrefab;
                                levelName = levelNode.GetComponent<LevelPrefab>().levelPrefab.name;

                                endIndex = int.Parse(levelNode.name.Substring(levelNode.name.Length - 2)) * 2;

                                Vector3[] path = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];
                                path = GetPath(startIndex, endIndex);

                                //Move the object to the specified location using the sub-path.
                                iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 5, "orienttopath", true, "easetype", iTween.EaseType.easeInOutSine));

                                //Character is on the move.
                                isMoving = true;
                            }
                        }
                    }
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

    public void MoveToNextLevel(GameObject currentLevel, GameObject nextLevel)
    {
        levelNode = nextLevel;
        levelPrefab = levelNode.GetComponent<LevelPrefab>().levelPrefab;
        levelName = levelNode.GetComponent<LevelPrefab>().levelPrefab.name;

        startIndex = int.Parse(currentLevel.name.Substring(levelNode.name.Length - 2)) * 2;
        endIndex = int.Parse(nextLevel.name.Substring(levelNode.name.Length - 2)) * 2;

        //Move the object to the specified location using the sub-path between startindex and endindex.
        Vector3[] path = new Vector3[Mathf.Abs(endIndex - startIndex) + 1];
        path = GetPath(startIndex, endIndex);
        iTween.MoveTo(player.gameObject, iTween.Hash("path", path, "time", 5, "orienttopath", true, "easetype", iTween.EaseType.easeInOutSine));

        //Set level index to the new level number.
        PlayerPrefs.SetInt("LevelIndex", endIndex / 2);

        isMoving = true;
    }

    bool isNodeVisible(int levelIndex)
    {
        GameObject level = GameObject.Find("Level " + levelIndex);

        if (level.GetComponent<LevelPrefab>().isVisible)
        {
            return true;
        }

        return false;
    }
}
