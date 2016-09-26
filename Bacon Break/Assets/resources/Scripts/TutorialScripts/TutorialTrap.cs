using UnityEngine;
using System.Collections;

public class TutorialTrap : MonoBehaviour {
    public GameObject destroyThis;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tutorial")
        {
            Destroy(destroyThis);
        }
    }
}
