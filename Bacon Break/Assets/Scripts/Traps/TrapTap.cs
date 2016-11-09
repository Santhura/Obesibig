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
    public bool releaseChaser = false;
    bool unleash;
    public GameObject destroyThis;
    public GameObject unleashThis;
    public GameObject PS_explosion;

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
        if (unleash && gameObject.name == "LooseSawTrap")
        {
            unleashThis.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }

        if (releaseChaser && gameObject.name == "ChasingSawTrap")
        {
            Debug.Log("released...RUNN!!");
            if(unleashThis != null)
                unleashThis.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
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
            GameObject explosion = Instantiate(PS_explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 2);
            Destroy(destroyThis);
        }

        if (canUnleash && !destroyThis)
        {
            transform.tag = "Untagged";
            Destroy(transform.GetComponent<BoxCollider>());
            addScore.trapsDestroyedAmount += 1;
            SelectedAudio.clip = destroySounds[Random.Range(0, destroySounds.Length)];
            SelectedAudio.Play();
            unleash = true;
        }

        Debug.Log(gameObject.name);

        //When tapped the chasing chain saw can be released
        if (gameObject.name == "ChasingSawTrap")
        {
            releaseChaser = true;
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
