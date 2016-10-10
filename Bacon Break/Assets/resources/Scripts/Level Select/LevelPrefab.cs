using UnityEngine;
using System.Collections;

public class LevelPrefab : MonoBehaviour
{
    public GameObject levelPrefab;
    private int unlocked;
    
    void Start() 
    {
        if (PlayerPrefs.HasKey(gameObject.name + "_unlocked"))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    //Unlock level
    public void Unlock()
    {
        unlocked = 1;
        PlayerPrefs.SetInt(gameObject.name + "_unlocked", unlocked);
    }

    //Check if level is unlocked
    public int isUnlocked()
    {
        unlocked = PlayerPrefs.GetInt(gameObject.name + "_unlocked");
        return unlocked;
    }
}
