﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;


//Used for basic tappables only. E.g. loosesawblade & cutter trap.
public class TrapTap : MonoBehaviour
{
//    public bool movementStoppable = false;

    public HighscoreManager addScore;
    public bool canUnleash = false;
    public bool releaseChaser = false;
    bool unleash;
    public GameObject destroyThis;
    public GameObject unleashThis;
    public float speed;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        addScore = GameObject.Find("Score Manager").GetComponent<HighscoreManager>();
        rb = this.GetComponent<Rigidbody>();
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
            unleashThis.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }
    void OnMouseDown()
    {
        if (/*!movementStoppable &&*/ !canUnleash)
        {
            addScore.trapsDestroyedAmount += 1;

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

        if (canUnleash)
        {
            transform.tag = "Untagged";
            Destroy(transform.GetComponent<BoxCollider>());
            addScore.trapsDestroyedAmount += 1;
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
