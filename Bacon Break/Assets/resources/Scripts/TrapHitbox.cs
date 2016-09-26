using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrapHitbox : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
            //SceneManager.LoadScene(0);
            WinOrLoseScript.isDead = true;
            PlayerMovement.isAbleToMove = false;
        }
    }

}
