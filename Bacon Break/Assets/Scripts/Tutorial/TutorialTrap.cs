using UnityEngine;
using System.Collections;

public class TutorialTrap : MonoBehaviour
{
    public GameObject destroyIndicator;
    public GameObject activateIndicator;
    void OnMouseDown()
    {
        Destroy(destroyIndicator);
        activateIndicator.gameObject.SetActive(true);
    }
}
