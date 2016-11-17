using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    //Public vars
    [Header("UI SHIT")]
    public GameObject pnl_levelInfo;        //For accessing and filling in the level's information
    public GameObject pnl_play;                 //Play the selected level
    public Text txt_levelName;              //For displaying the name of the level
    public RectTransform canvasRect;        //Canvas space
    public int clampOffsetX, clampOffsetY;  //Used for extra spacing if the panel had to be repositioned (when panel was (partly) out of the screen)

    //Private vars
    private Button btn_play;
    private Text txt_score;                 //For displaying the highscore;
    private Vector2 screenspaceWorld;       //Get world corners in screen space.

    void Awake()
    {
        Time.timeScale = 1.0f;
        btn_play = pnl_play.transform.GetChild(0).GetComponent<Button>();
        txt_score = pnl_levelInfo.transform.GetChild(0).GetComponent<Text>();
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
        //Check if level exists before unlocking it.
        if (PlayerPrefs.GetInt("Unlock") == 1 && GameObject.Find("Level " + (LevelNodeCollection.currentLevelIndex + 1)) != null)
        {
            if (GameObject.Find("Level " + (LevelNodeCollection.currentLevelIndex + 1)).activeSelf)
            {
                //Unlock level with index.
                GameObject level = GameObject.Find("Level " + PlayerPrefs.GetInt("LevelIndex"));
                GameObject gameManager = GameObject.Find("Game Manager");
                gameManager.GetComponent<UnlockLevel>().UnlockNextLevel(LevelNodeCollection.currentLevelIndex + 1);
                level.GetComponent<LevelPrefab>().Unlock();
                Debug.Log(LevelNodeCollection.currentLevelIndex + 1);
                //Zet big op levelnode;
                GameObject.FindWithTag("Player").transform.position = level.transform.position;

                //Level is unlocked, set unlock to false again.
                PlayerPrefs.SetInt("Unlock", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Unlock", 0);
            }

        }
        else
        {
            PlayerPrefs.SetInt("Unlock", 0);
        }
    }

    //TODO:: Add prefab name
    public void SetLevelInformation(Vector3 buttonPosition, string levelName, GameObject levelPrefab, int levelIndex, int score)
    {
        //Set information panel to the levelnode's position
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(buttonPosition.x, buttonPosition.y, buttonPosition.z + 2));
        pnl_play.transform.position = screenPoint;

        //"Clamp" panel if it happens to be positioned (partially) out of the screen view.
        //ClampPanel();

        //Set level name and prefab
        txt_levelName.text = levelName;
        txt_score.text = "Score: " + score.ToString();
        btn_play.GetComponent<LevelSelector>().SetLevelObject(levelPrefab, levelIndex);

        pnl_levelInfo.SetActive(true);
        pnl_play.SetActive(true);
    }

    public void ClampPanel()
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
