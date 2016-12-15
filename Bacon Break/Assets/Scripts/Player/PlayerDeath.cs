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
                    if (deathObjects[i].name == "Death_Axe") {
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
