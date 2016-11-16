using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnlockLevel : MonoBehaviour
{
    //Level to unlock
    private List<GameObject> levelList = new List<GameObject>();

    private GameObject curLevel;
    private GameObject nextLevel;
    private GameObject strtLevel;
    private int unlockedLevels;
    private int curLevelIndex, nxtLevelIndex;

    //Reveal the path to the next level and unlock it.
    public void UnlockNextLevel(int nextLevelIndex)
    {
        GameObject level = new GameObject();
        levelList.Clear();

        //Find the "next" level (the last unlocked level)
        curLevel = GameObject.Find("Level " + PlayerPrefs.GetInt("LevelIndex"));
        nextLevel = GameObject.Find("Level " + nextLevelIndex);
        unlockedLevels = nextLevelIndex - PlayerPrefs.GetInt("LevelIndex");

        //Create a list for revealing level paths
        for (int i = 0; i <= unlockedLevels; i++)
        {
            level = GameObject.Find("Level " + (PlayerPrefs.GetInt("LevelIndex") + i));
            levelList.Add(level);
            StartCoroutine(RevealPath(i));
        }

        //Move the character to the last unlocked level
        GameObject.Find("Game Manager").GetComponent<NodeMovement>().MoveToNextLevel(curLevel, nextLevel);
        nextLevel.GetComponent<LevelPrefab>().Unlock();
    }

    //Nice enumerator to make the path appear one piece at a time.
    IEnumerator RevealPath(int i)
    {
        //Reveil the level paths of all unlocked levels while in-game
        for (int j = 0; j < levelList[i].transform.childCount; j++)
        {
            levelList[i].transform.GetChild(j).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
