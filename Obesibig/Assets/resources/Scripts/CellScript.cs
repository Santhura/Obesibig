using UnityEngine;
using System.Collections;

public class CellScript : MonoBehaviour
{

    private float speed = 2.5f;               // the speed of a cell
    private GameObject[] target;            // all organ gameobjects positions
    GameObject closest;

    [SerializeField]
    private bool isRandomTarget;            // Does it choose a random target
    private GameObject realRandomTarget;    // the real target when it chooses a random target

    private bool isTargetDead;              // check to see if target is dead

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Organ");
        isTargetDead = false;
        if (isRandomTarget)
            realRandomTarget = RandomTarget();

    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetDead)
        {
            isRandomTarget = false;
        }
        if (target.Length > 0)
        {
            if (!isRandomTarget)
                transform.position = Vector3.MoveTowards(transform.position, FindClosestTarget().transform.position, speed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, realRandomTarget.transform.position, speed * Time.deltaTime);
        }
        Debug.Log(target.Length);
    }


    /// <summary>
    /// Find all the organs and find the closest one
    /// </summary>
    /// <returns> closest target (organ) </returns>
    GameObject FindClosestTarget()
    {
        target = GameObject.FindGameObjectsWithTag("Organ");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject gameObject in target)
        {
            Vector3 diff = gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }

    /// <summary>
    /// return a random organ object.
    /// </summary>
    /// <returns></returns>
    GameObject RandomTarget()
    {
        GameObject[] rTargets;
        rTargets = GameObject.FindGameObjectsWithTag("Organ");
        GameObject realTarget;
        realTarget = rTargets[Random.Range(0, rTargets.Length)];
        return realTarget;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Organ")
        {
            isTargetDead = true;
            Destroy(other.gameObject);
        }
    }
}
