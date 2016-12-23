using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class MainMenuEvents : MonoBehaviour
{

    private Text signInButtonText;
    public GameObject achButton;
    public GameObject leaderboardButton;


    // Use this for initialization
    void Start()
    {
        signInButtonText = GameObject.Find("Sign_In").GetComponentInChildren<Text>();

        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Show achievements and leaderboards button if authenticated
        achButton.SetActive(Social.localUser.authenticated);
        leaderboardButton.SetActive(Social.localUser.authenticated);
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            signInButtonText.text = "Sign In";
        }
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            // Change sign-in button text
            signInButtonText.text = "Sign out";
        }
        else
        {
            // Do not change sign-in button text
            signInButtonText.text = "Sign in";
        }
    }

    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("Cannot show Achievements: not authenticated");
        }
    }

    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }
}
