using UnityEngine;
using System.Collections;

public class CellScript : MonoBehaviour
{

    private float speed = 2.5f;              // the speed of a cell
    private GameObject[] target;             // all organ gameobjects positions
    GameObject closest;                      // Is the closest object for the cell

    [SerializeField]
    private bool isRandomTarget;             // Does it choose a random target
    private GameObject realRandomTarget;     // the real target when it chooses a random target

    private bool isTargetDead;               // check to see if target is dead

    [SerializeField]
    private string targetTagName = "Organ";  // The target tag name

    public GameObject organParticle;         // show particle when organ dies

    private GameObject[] allCells;           // check how many cells there are

    // Use this for initialization
    void Start()
    {
        allCells = GameObject.FindGameObjectsWithTag("Cell");
        target = GameObject.FindGameObjectsWithTag(targetTagName);
        isTargetDead = false;
        if (isRandomTarget)
            realRandomTarget = RandomTarget();
    }

    void Update()
    {
        // when first random target is dead, stop with the randomtarget.
        if (isTargetDead)
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                allCells[i].GetComponent<CellScript>().isRandomTarget = false;
            }
            isTargetDead = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move to random target or closest target, as long as there are targets
        if (target.Length > 0 || target != null)
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
    
    /// <summary>
    /// When cell is touching organ decrease health of organ. untill it's dead
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay(Collision other)
    {
        // drops health as long cell is touching the organ
        if (other.gameObject.tag == targetTagName)
        {
            if (other.gameObject.GetComponent<OrganHealthScript>() != null)
                other.gameObject.GetComponent<OrganHealthScript>().health -= other.gameObject.GetComponent<OrganHealthScript>().decreasement * Time.deltaTime;

            // if organs health drop to 0 destroy organ
            if (other.gameObject != null)
            {
                if (other.gameObject.GetComponent<OrganHealthScript>().health <= 0)
                {
                    isTargetDead = true;
                    Instantiate(organParticle, other.transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
