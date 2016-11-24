using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrapHitbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < GetComponents<Collider>().Length; i++)
            {
                GetComponents<Collider>()[i].enabled = false;
            }

            PlayerMovement.isAbleToMove = false;
            WinOrLoseScript.isDead = true;
        }

        if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 0.5f);
        }
    }

}
