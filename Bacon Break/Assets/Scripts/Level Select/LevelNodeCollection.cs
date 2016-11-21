using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelNodeCollection : MonoBehaviour {

    private static LevelNodeCollection levelNodeCollection;         //singleton

    public static List<string> levelNames = new List<string>();     // a list of all the level names
    public static string currentLevelName;                          // current level that is playing
    public static int currentLevelIndex;                            // current index of the array of levels

    private List<GameObject> levels = new List<GameObject>();       // a list to find all level objects

    private GameManager gameManager;                                // for a check in which current scene is playing

    protected void Start() {
        if (!levelNodeCollection) {
            levelNodeCollection = this;
            Object.DontDestroyOnLoad(gameObject);                   // always run this script
        }
        else {
            Destroy(gameObject);
        }
        gameManager = GetComponent<GameManager>();
    }

    protected void Update() {
        // when in level select, find all the levels and get al the level names
        if(gameManager.CurrentSceneName == "v2LevelSelect") {
            FindLevels();
        }
    }

    /// <summary>
    /// Get all the level names in level select
    /// </summary>
    private void FindLevels() {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Node").Length; i++) {

            if (GameObject.Find("Level " + i) != null) {
                levels.Add(GameObject.Find("Level " + i));
            }
        }

        for (int i = 0; i < levels.Count; i++) {
            levelNames.Add(levels[i].GetComponent<LevelPrefab>().levelPrefab.name);
        }
    }
}
