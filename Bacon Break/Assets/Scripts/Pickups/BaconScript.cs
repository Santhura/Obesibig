using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;

public class BaconScript : MonoBehaviour
{
    public AudioClip baconSound;
    private AudioSource SelectedAudio;
    public GameObject pnl_score;           //UI element for displaying the total score (of bacon).
    public GameObject bar_stamina;         //UI element displaying the amount of stamina.

    // Use this for initialization.
    void Start()
    {
        pnl_score = GameObject.Find("pnl_score");
        bar_stamina = GameObject.Find("bar_stamina");
        SelectedAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame.
    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        //Add bacon points and stamina when the player collides with the bacon object.
        //Destroy the bacon object.
        if (col.gameObject.tag == "Player")
        {
            bar_stamina.GetComponent<StaminaScript>().AddStamina();
            pnl_score.GetComponent<ScoreScript>().AddBacon();
            SelectedAudio.clip = baconSound;
            SelectedAudio.Play();

            // Only unlock achievements if the user is signed in.
            if (Social.localUser.authenticated)
            {
                //Unlock the "Welcome to Bacon Break" achievement
                PlayGamesPlatform.Instance.ReportProgress(
                    GPGSIds.achievement_welcome_to_bacon_break,
                    100.0f, (bool success) =>
                    {
                        Debug.Log("(Bacon Break) Welcome Unlock: " +
                              success);
                    });
            }

            Destroy(gameObject);
        }
    }
}
