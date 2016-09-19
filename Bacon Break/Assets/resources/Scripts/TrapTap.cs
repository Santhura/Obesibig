using UnityEngine;
using System.Collections;

public class TrapTap : MonoBehaviour
{
    public bool movementStoppable = false;

    public bool canUnleash = false;
    bool unleash;

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
            transform.Translate(Vector3.left * Time.deltaTime * 5, Space.World);
        }
    }
    void OnMouseDown()
    {
        if (!movementStoppable && !canUnleash)
            Destroy(this.gameObject);

        if (movementStoppable)
            rb.isKinematic = true;

        if (canUnleash)
        {
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
