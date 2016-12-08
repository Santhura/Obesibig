using UnityEngine;
using GooglePlayGames;
using System.Collections;

public class LevelPrefab : MonoBehaviour
{
    public GameObject levelPrefab;
    private int unlocked;
    public bool isVisible;

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
        if (!PlayerPrefs.HasKey(gameObject.name + "_unlocked"))
        {
            if (Social.localUser.authenticated)
            {
                // Increment the "Fitness Master" achievement.
                // This achievement is unlocked after 6 completed levels (6 increments).
                PlayGamesPlatform.Instance.IncrementAchievement(
                       GPGSIds.achievement_fitness_master,
                       1,
                       (bool success) =>
                       {
                           Debug.Log("(Bacon Break) Fitness Master Increment: " +
                              success);
                       });
            }

            unlocked = 1;
            PlayerPrefs.SetInt(gameObject.name + "_unlocked", unlocked);
        }
    }

    //Check if level is unlocked
    public int isUnlocked()
    {
        unlocked = PlayerPrefs.GetInt(gameObject.name + "_unlocked");
        return unlocked;
    }

    void OnBecameInvisible()
    {
        this.isVisible = false;
    }

    void OnBecameVisible()
    {
        this.isVisible = true;
    }

}
