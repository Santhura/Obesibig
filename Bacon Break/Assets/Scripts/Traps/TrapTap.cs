using UnityEngine;
using System.Collections;
using GooglePlayGames;


//Used for basic tappables only. E.g. loosesawblade & cutter trap.
public class TrapTap : MonoBehaviour
{
    //    public bool movementStoppable = false;
    public AudioClip[] destroySounds;
    private AudioSource SelectedAudio;
    public float speed = 40;

    public HighscoreManager addScore;
    public bool canUnleash = false;
    bool unleash;
    public GameObject destroyThis;
    public GameObject unleashThis;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        addScore = GameObject.Find("Score Manager").GetComponent<HighscoreManager>();
        rb = this.GetComponent<Rigidbody>();
        SelectedAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unleash)
        {
            unleashThis.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }
    }
    void OnMouseDown()
    {
        if (!canUnleash && destroyThis)
        {
            addScore.trapsDestroyedAmount += 1;
            SelectedAudio.clip = destroySounds[Random.Range(0, destroySounds.Length)];
            SelectedAudio.Play();

            transform.tag = "Untagged";

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

            Destroy(destroyThis);
        }

    //    if (movementStoppable)
    //        rb.isKinematic = true;

        if (canUnleash && !destroyThis)
        {
            transform.tag = "Untagged";
            Destroy(transform.GetComponent<BoxCollider>());
            addScore.trapsDestroyedAmount += 1;
            SelectedAudio.clip = destroySounds[Random.Range(0, destroySounds.Length)];
            SelectedAudio.Play();
            unleash = true;
        }
    }
 /*
    void OnMouseUp()
    {
        if (movementStoppable)
            rb.isKinematic = false;
    }
    void OnMouseExit()
    {
        if (movementStoppable)
            rb.isKinematic = false;
    }
    */
}
