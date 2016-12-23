using UnityEngine;
using System.Collections;
using GooglePlayGames;

public static class Achievement
{
    //Unlock (non-incremental) achievement
    public static void Unlock(string name)
    {
        //Check if the achievement is really non-incremental
        if (!PlayGamesPlatform.Instance.GetAchievement(name).IsIncremental)
        {
            // Only unlock achievements if the user is signed in.
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportProgress(
                    name,
                    100.0f, (bool success) =>
                    {
                        Debug.Log(name + " unlock: " +
                              success);
                    });
            }
        }
        else
        {
            //The achievement must be an incremental one, increment instead
            Debug.LogError("The achievement " + name + " is incremental!");
            Increment(name);
        }
    }

    //Update incremental achievement
    public static void Increment(string name)
    {
        //Check if the achievement is really incremental
        if (PlayGamesPlatform.Instance.GetAchievement(name).IsIncremental)
        {
            Debug.Log("Current increment step: " + PlayGamesPlatform.Instance.GetAchievement(name).CurrentSteps);

            // Only update achievement if the user is signed in.
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(
                       name,
                       1,
                       (bool success) =>
                       {
                           Debug.Log(name + " increment: " +
                              success);
                       });
            }
        }
        else
        {
            //The achievement must be a non-incremental one, unlock instead
            Debug.LogError("The achievement " + name + " is non-incremental!");
            Unlock(name);
        }
    }
}
