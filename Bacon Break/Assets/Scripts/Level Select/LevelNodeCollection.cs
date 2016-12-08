﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelNodeCollection : MonoBehaviour
{

    private static LevelNodeCollection levelNodeCollection;         //singleton

    public static List<string> levelNames = new List<string>();     // a list of all the level names
    public static string currentLevelName;                          // current level that is playing
    public static int currentLevelIndex;                            // current index of the array of levels

    private List<GameObject> levels = new List<GameObject>();       // a list to find all level objects
    public static List<string> nodeNames = new List<string>();

    private GameManager gameManager;                                // for a check in which current scene is playing
    private bool levelsFound;                                       // to check if the levels have been found

    protected void Start()
    {
        levelsFound = false;
        gameManager = GetComponent<GameManager>();
    }

    protected void Update()
    {
        // when in level select, find all the levels and get al the level names
        if (gameManager.CurrentSceneName == "v2LevelSelect" && !levelsFound)
        {
            FindLevels();
            levelsFound = true;
        }
    }

    /// <summary>
    /// Get all the level names in level select
    /// </summary>
    private void FindLevels()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LevelIndex");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Node").Length; i++)
        {
            GameObject level = GameObject.Find("Level " + i);

            if (level != null)
            {
                levels.Add(level);
                nodeNames.Add(level.name);
            }
        }

        for (int i = 0; i < levels.Count; i++)
        {
            levelNames.Add(levels[i].GetComponent<LevelPrefab>().levelPrefab.name);
        }
    }
}
