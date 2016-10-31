using UnityEngine;
using System.Collections;

public class StopTrapAnimation : MonoBehaviour
{
    public Animation trapAnimation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        trapAnimation.enabled = false;
    }
}
