using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{
    float movementSpeed = 7.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            this.transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }
}
