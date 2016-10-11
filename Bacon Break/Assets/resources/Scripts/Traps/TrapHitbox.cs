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
            CameraShake.isHit = true;
            for (int i = 0; i < GetComponents<Collider>().Length; i++) {
                GetComponents<Collider>()[i].enabled = false;
            }
            PlayerMovement.isAbleToMove = false;
            CameraShake.deathParticle.SetActive(true);
        }
    }

}
