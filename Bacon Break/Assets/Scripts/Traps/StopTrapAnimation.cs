using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class StopTrapAnimation : MonoBehaviour
{
    public AudioClip destroySounds;
    public HighscoreManager addScore;
    public Animation trapAnimation;
    private AudioSource SelectedAudio;
    // Use this for initialization

    public bool activated;

    void Start()
    {
        activated = false;

        SelectedAudio = GetComponent<AudioSource>();
        addScore = GameObject.Find("Score Manager").GetComponent<HighscoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Tapped()
    {
        if (!TrapRay.hasPressed)
        {
            foreach (AnimationState state in trapAnimation)
            {
                if (state.speed > 0)
                    state.speed = 0f;
                else
                    state.speed = 1.0f;
            }
        }

        if (!activated)addScore.trapsDestroyedAmount += 1;
        activated = true;

        SelectedAudio.enabled = true;
        SelectedAudio.clip = destroySounds;
        SelectedAudio.loop = false;
        SelectedAudio.Play();


        // Only unlock achievements if the user is signed in.
        if (Social.localUser.authenticated)
        {
            // Increment the "Trap Novice" achievement.
            PlayGamesPlatform.Instance.IncrementAchievement(
                   GPGSIds.achievement_trap_novice,
                   1,
                   (bool success) =>
                   {
                       Debug.Log("(Bacon Break) Trap Novice Increment: " +
                          success);
                   });
        }
    }
}
