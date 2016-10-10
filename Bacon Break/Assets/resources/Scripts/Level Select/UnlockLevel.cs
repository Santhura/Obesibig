﻿using UnityEngine;
using System.Collections;

public class UnlockLevel : MonoBehaviour
{
    //Level to unlock
    private GameObject curLevel;
    private GameObject nextLevel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            UnlockNextLevel(0);
        }
    }

    //Reveal the path to the next level and unlock it.
    public void UnlockNextLevel(int nextLevelIndex)
    {
        //curLevel = GameObject.Find("Level " + curLevelIndex);
        nextLevel = GameObject.Find("Level " + nextLevelIndex);

        StartCoroutine(RevealPath());

        nextLevel.GetComponent<LevelPrefab>().Unlock();
    }

    //Nice enumerator to make the path appear one piece at a time.
    IEnumerator RevealPath()
    {
        for (int i = 0; i < nextLevel.transform.childCount; i++)
        {
            nextLevel.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
