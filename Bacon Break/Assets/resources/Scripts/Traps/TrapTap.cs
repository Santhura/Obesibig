using UnityEngine;
using System.Collections;

public class TrapTap : MonoBehaviour
{
    public bool movementStoppable = false;

    public bool canUnleash = false;
    bool unleash;
    public GameObject destroyThis;
    public GameObject unleashThis;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unleash)
        {
            unleashThis.transform.Translate(Vector3.back * Time.deltaTime * 25, Space.World);
        }
    }
    void OnMouseDown()
    {
        if (!movementStoppable && !canUnleash)
        {
            transform.tag = "Untagged";
            Destroy(destroyThis);
        }

        if (movementStoppable)
            rb.isKinematic = true;

        if (canUnleash)
        {
            transform.tag = "Untagged";
            Destroy(transform.GetComponent<BoxCollider>());
            unleash = true;
        }
    }
    void OnMouseUp()
    {
        if (movementStoppable)
            rb.isKinematic = false;
    }
    void OnMouseExit()
    {
        if (movementStoppable)
            rb.isKinematic = false;
    }
}
