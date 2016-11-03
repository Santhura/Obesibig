using UnityEngine;
using System.Collections;
using GooglePlayGames;

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
    }

    //Reveal the path to the next level and unlock it.
    public void UnlockNextLevel(int nextLevelIndex)
    {
        if (Social.localUser.authenticated)
        {
            // Increment the "Fitness Master" achievement.
            // This achievement is unlocked after 10 completed levels (10 increments).
            PlayGamesPlatform.Instance.IncrementAchievement(
                   GPGSIds.achievement_fitness_master,
                   1,
                   (bool success) =>
                   {
                       Debug.Log("(Bacon Break) Fitness Master Increment: " +
                          success);
                   });
        }

        curLevel = GameObject.Find("Level " + (nextLevelIndex - 1));
        nextLevel = GameObject.Find("Level " + nextLevelIndex);

        StartCoroutine(RevealPath());

        GameObject.Find("Game Manager").GetComponent<NodeMovement>().MoveToNextLevel(curLevel, nextLevel);

        nextLevel.GetComponent<LevelPrefab>().Unlock();
    }

    //Nice enumerator to make the path appear one piece at a time.
    IEnumerator RevealPath()
    {
        for (int i = 0; i < nextLevel.transform.childCount; i++)
        {
            nextLevel.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
