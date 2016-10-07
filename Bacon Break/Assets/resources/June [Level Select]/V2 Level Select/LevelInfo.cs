using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    //Public vars
    public GameObject pnl_levelInfo;        //For accessing and filling in the level's information
    public RectTransform canvasRect;        //Canvas space
    public int clampOffsetX, clampOffsetY;  //Used for extra spacing if the panel had to be repositioned (when panel was (partly) out of the screen)

    //Private vars
    private Text txt_levelName;             //For displaying the name of the level
    private Button btn_play;                //Play a level!
    private Vector2 screenspaceWorld;       //Get world corners in screen space.

    void Awake()
    {
        txt_levelName = pnl_levelInfo.transform.GetChild(0).GetComponent<Text>();
        btn_play = pnl_levelInfo.transform.GetChild(1).GetComponent<Button>();
    }

    // Use this for initialization
    void Start()
    {
        clampOffsetX = 20;
        clampOffsetY = 20;       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO:: Add prefab name
    public void SetLevelInformation(Vector3 panelPosition, string levelName, GameObject levelPrefab)
    {
        //Set information panel to the levelnode's position
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, panelPosition);
        pnl_levelInfo.transform.position = screenPoint;

        //"Clamp" panel if it happens to be positioned (partially) out of the screen view.
        ClampPanel();

        //Set level name and prefab
        txt_levelName.text = levelName;
        btn_play.GetComponent<LevelSelector>().SetLevelObject(levelPrefab);

        pnl_levelInfo.SetActive(true);
    }

    private void ClampPanel()
    {
        //Get minimum values of the panel (left, bottom)
        Vector2 min = pnl_levelInfo.GetComponent<RectTransform>().anchorMin;
        min.x *= Screen.width;
        min.y *= Screen.height;
        min += pnl_levelInfo.GetComponent<RectTransform>().offsetMin;

        //Get maximum values of the panel (right, top)
        Vector2 max = pnl_levelInfo.GetComponent<RectTransform>().anchorMax;
        max.x *= Screen.width;
        max.y *= Screen.height;
        max += pnl_levelInfo.GetComponent<RectTransform>().offsetMax;

        //Reposition panel if a part of it isn't visible on the screen
        //Panel space below the screen.
        if (min.y < canvasRect.offsetMin.y)
        {
            pnl_levelInfo.transform.position += new Vector3(0, Mathf.Abs(canvasRect.offsetMin.y - min.y) + clampOffsetY, 0);
        }

        //Panel too much to the left.
        if (min.x < canvasRect.offsetMin.x)
        {
            pnl_levelInfo.transform.position += new Vector3(Mathf.Abs(canvasRect.offsetMin.x - min.x) + clampOffsetX, 0, 0);
        }

        //Panel space above the screen.
        if (max.y > canvasRect.offsetMax.y)
        {
            pnl_levelInfo.transform.position -= new Vector3(0, Mathf.Abs(canvasRect.offsetMax.y - max.y) + clampOffsetY, 0);
        }

        //Panel too much to the right.
        if (max.x > canvasRect.offsetMax.x)
        {
            pnl_levelInfo.transform.position -= new Vector3(Mathf.Abs(canvasRect.offsetMax.x - max.x) + clampOffsetX, 0, 0);
        }
    }
}
