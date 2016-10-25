using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class MainMenuEvents : MonoBehaviour
{

    private Text signInButtonText;
    private Text authStatus;
    private GameObject achButton;
    private GameObject leaderboardButton;


    // Use this for initialization
    void Start()
    {
        signInButtonText = GameObject.Find("Sign_In").GetComponentInChildren<Text>();
        authStatus = GameObject.Find("authStatus").GetComponent<Text>();
        achButton = GameObject.Find("Achievements_Button");
        leaderboardButton = GameObject.Find("Leaderboard_Button");

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
        //Show achievements if authenticated
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
            authStatus.text = "";
        }
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Bacon Break) Signed in!");

            // Change sign-in button text
            signInButtonText.text = "Sign out";

            // Show the user's name
            authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Bacon Break) Sign-in failed...");

            // Show failure message
            signInButtonText.text = "Sign in";
            authStatus.text = "Sign-in failed";
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
            Debug.Log("Cannot show Achievements, not logged in");
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
