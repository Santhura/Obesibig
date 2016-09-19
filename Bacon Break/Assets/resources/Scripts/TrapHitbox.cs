using UnityEngine;
using System.Collections;

public class TrapHitbox : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            Destroy(other.gameObject);
        }
    }

}
