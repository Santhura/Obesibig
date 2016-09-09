using UnityEngine;
using System.Collections;

public class CellScript : MonoBehaviour
{

    private float speed = 2.5f;               // the speed of a cell
    private GameObject[] target;             // all organ gameobjects positions
    GameObject closest;

    [SerializeField]
    private bool isRandomTarget;             // Does it choose a random target
    private GameObject realRandomTarget;     // the real target when it chooses a random target

    private bool isTargetDead;               // check to see if target is dead

    [SerializeField]
    private string targetTagName = "Organ";  // The target tag name

    public GameObject organParticle;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag(targetTagName);
        isTargetDead = false;
        if (isRandomTarget)
            realRandomTarget = RandomTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
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
    }


    /// <summary>
    /// Find all the organs and find the closest one
    /// </summary>
    /// <returns> closest target (organ) </returns>
    GameObject FindClosestTarget()
    {
        target = GameObject.FindGameObjectsWithTag(targetTagName);
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
    /// <returns> target to move to </returns>
    GameObject RandomTarget()
    {
        GameObject[] rTargets;
        rTargets = GameObject.FindGameObjectsWithTag(targetTagName);
        GameObject realTarget;
        realTarget = rTargets[Random.Range(0, rTargets.Length)];
        return realTarget;
    }
    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == targetTagName)
        {
            isTargetDead = true;
            Instantiate(organParticle, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
