using UnityEngine;
using System.Collections;

public class TutorialTrap : MonoBehaviour
{
    public GameObject destroyIndicator;
    public GameObject activateIndicator;
    void OnMouseDown()
    {
        if (destroyIndicator)
        Destroy(destroyIndicator);

        if(activateIndicator)
        activateIndicator.gameObject.SetActive(true);
    }
}
