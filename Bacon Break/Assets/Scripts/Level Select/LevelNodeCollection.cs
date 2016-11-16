using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelNodeCollection : MonoBehaviour {

    private static LevelNodeCollection levelNodeCollection;

    public static List<string> levelNames = new List<string>();
    public static string currentLevelName;

    private GameObject[] levels;

    protected void Start() {
        Object.DontDestroyOnLoad(gameObject);
        levelNodeCollection = this;

        levels = GameObject.FindGameObjectsWithTag("Node");

        for (int i = 0; i < levels.Length; i++) {
            levelNames.Add(levels[i].GetComponent<LevelPrefab>().levelPrefab.name);
        }
    }
}
