using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelNodeCollection : MonoBehaviour {

    private static LevelNodeCollection levelNodeCollection;

    public static List<string> levelNames = new List<string>();
    public static string currentLevelName;
    public static int currentLevelIndex;

    private List<GameObject> levels = new List<GameObject>();

    private GameManager gameManager;

    protected void Start() {
        if (!levelNodeCollection) {
            levelNodeCollection = this;
            Object.DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        gameManager = GetComponent<GameManager>();
    }

    protected void Update() {
        if(gameManager.CurrentSceneName == "v2LevelSelect") {
            FindLevels();
        }
    }


    private void FindLevels() {
        Debug.Log("askdfjlasdfklj");
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
