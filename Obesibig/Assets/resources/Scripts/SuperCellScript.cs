using UnityEngine;
using System.Collections;

public class SuperCellScript : MonoBehaviour {

    private float speed = 1.5f;                // the speed of a supercell
    private GameObject[] target;             // all cells gameobjects positions
    GameObject closest;                      // Is the closest object for the supercell

    [SerializeField]
    private string targetTagName = "Cell";  // The target tag name

    private float timer = 5f;               // how long does supercell exist

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            if(FindClosestTarget() != null)
                transform.position = Vector3.MoveTowards(transform.position, FindClosestTarget().transform.position, speed * Time.deltaTime);
        }
        else
            Destroy(gameObject);

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

    void OnCollisionStay(Collision other)
    {
        // drops health as long cell is touching the organ
        if (other.gameObject.tag == targetTagName)
        {
            Destroy(other.gameObject);
        }
    }
}
