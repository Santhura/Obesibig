using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {


    public GameObject[] deathObjects;                   // all the meshes from the death animations
        
    private Animator animator;                                  // Triger the right animation
    public SkinnedMeshRenderer skinnedMeshRenderer;             // Disable the main renderer

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        for (int i = 0; i < deathObjects.Length; i++) {
            deathObjects[i].SetActive(false);
        }
	}
	
    public void TriggerDeathAnimation(string trapTag) {
        skinnedMeshRenderer.enabled = false;

        switch(trapTag) {
            case "AxeTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if (deathObjects[i].name == "Cut in half")
                        deathObjects[i].SetActive(true);
                }
                break;
            case "CutterTrap":
                break;
            case "HammerTrap":
                break;
            case "LooseSawTrap":
                for (int i = 0; i < deathObjects.Length; i++) {
                    if(deathObjects[i].name == "SlicePlayer")
                        deathObjects[i].SetActive(true);
                }
                break;
            case "MovingSawTrap":
                break;
            default:
                break;
        }
       
    }
    
}
