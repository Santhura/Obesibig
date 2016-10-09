using UnityEngine;
using System.Collections;

public class LevelPrefab : MonoBehaviour
{
    public GameObject levelPrefab;
    private bool unlocked;

    //Unlock level
    public void Unlock()
    {
        unlocked = true;
    }

    //Check if level is unlocked
    public bool isUnlocked()
    {
        if (unlocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
