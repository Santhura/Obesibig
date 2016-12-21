using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {
    
    public GameObject[] deathObjects;                   // all the meshes from the death animations
    public SkinnedMeshRenderer skinnedMeshRenderer;     // Disable the main renderer
    public GameObject PS_blood;                         // activate blood particles

    private Animator animator;                          // Triger the right animation
    private Rigidbody rigidbody;                        // turn off gravity;
    private Collider player_collider;                   // enable the collider from the player;

    // Use this for initialization
    void Awake () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        player_collider = GetComponent<Collider>();
        for (int i = 0; i < deathObjects.Length; i++) {
            deathObjects[i].SetActive(false);
        }
        PS_blood.SetActive(false);
	}
	
    /// <summary>
    /// Activate the right death animation by checking the right tag.
    /// </summary>
    /// <param name="trapTag"></param>
    public void TriggerDeathAnimation(string trapTag) {
        rigidbody.useGravity = false;
        player_collider.enabled = false;
        skinnedMeshRenderer.enabled = false;
        PS_blood.SetActive(true);
        switch(trapTag) {
            case "AxeTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if (deathObjects[i].name == "Death_Axe") {
                        deathObjects[i].SetActive(true);
                        break;
                    }
                }
                break;
            case "CutterTrap":
                break;
            case "HammerTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if (deathObjects[i].name == "Death_Hammer") {
                        deathObjects[i].SetActive(true);
                        break;
                    }
                }
                break;
            case "LooseSawTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if (deathObjects[i].name == "Death_LooseSaw") {
                        deathObjects[i].SetActive(true);
                        break;
                    }
                }
                break;
            case "MovingSawTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if (deathObjects[i].name == "Death_Axe") {      // do the same animation as for the axe death.
                        deathObjects[i].SetActive(true);
                        break;
                    }
                }
                break;
            default:
                break;
        }
       
    }
    
}
