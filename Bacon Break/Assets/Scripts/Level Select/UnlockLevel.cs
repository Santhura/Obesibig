using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnlockLevel : MonoBehaviour
{
    //Level to unlock
    private List<GameObject> levelList = new List<GameObject>();

    private GameObject curLevel;
    private GameObject nextLevel;
    private int unlockedLevels;

    //Reveal the path to the next level and unlock it.
    public void UnlockNextLevel(int nextLevelIndex)
    {
        levelList.Clear();

        //Find the "next" level (the last unlocked level)
        curLevel = GameObject.Find("Level " + PlayerPrefs.GetInt("LevelIndex"));
        nextLevel = GameObject.Find("Level " + nextLevelIndex);
        unlockedLevels = nextLevelIndex - PlayerPrefs.GetInt("LevelIndex");

        //Reveal path to last unlocked level
        StartCoroutine(RevealPath());

        //Move the character to the last unlocked level
        GameObject.Find("Game Manager").GetComponent<NodeMovement>().MoveToNextLevel(curLevel, nextLevel);
        nextLevel.GetComponent<LevelPrefab>().Unlock();
    }

    //Nice enumerator to make the path appear one piece at a time.
    IEnumerator RevealPath()
    {
        GameObject level = new GameObject();

        for (int i = 1; i <= unlockedLevels; i++)
        {
            level = GameObject.Find("Level " + (PlayerPrefs.GetInt("LevelIndex") + i));

            //Reveil the level paths of all unlocked levels while in-game
            for (int j = 0; j < level.transform.childCount; j++)
            {
                level.transform.GetChild(j).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
}
