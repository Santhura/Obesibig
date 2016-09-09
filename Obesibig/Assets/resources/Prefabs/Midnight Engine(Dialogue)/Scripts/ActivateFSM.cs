using UnityEngine;
using System.Collections;

public class ActivateFSM : MonoBehaviour {
    public GameObject theFSM;
    public bool activateWithCollision;
    public bool requireButtonPress;
    private bool waitForPress;
    public bool destroyWhenDone;


    void Update() {
        if (waitForPress && Input.GetButtonDown("Fire1"))
        {
            theFSM.SetActive(true);

            if (destroyWhenDone)
            this.gameObject.SetActive(false);

            waitForPress = false;
        }
                  }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (requireButtonPress)
            {
                waitForPress = true;
                return;

            }
            else if (activateWithCollision)
            {

                theFSM.SetActive(true);

                if (destroyWhenDone)
                this.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;

        }
    }

}
