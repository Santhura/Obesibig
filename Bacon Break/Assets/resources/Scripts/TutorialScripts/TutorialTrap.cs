using UnityEngine;
using System.Collections;

public class TutorialTrap : MonoBehaviour {
    public GameObject tutorialObject;
    public bool unleashThis;
    public bool unleashNow;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tutorial")
        {
            if (unleashThis)
                unleashNow = true;

            else
            {
                Destroy(tutorialObject);
            }
        }
    }

    void Update()
    {
        if (unleashNow)
            tutorialObject.transform.Translate(Vector3.back * Time.deltaTime * 15, Space.World);
    }
}
