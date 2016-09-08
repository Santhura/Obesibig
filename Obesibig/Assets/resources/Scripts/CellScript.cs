using UnityEngine;
using System.Collections;

public class CellScript : MonoBehaviour {

    private float speed = .5f;               // the speed of a cell
    private GameObject[] target;            // all organ gameobjects positions
    GameObject closest;

    // Use this for initialization
    void Start () {

      
    }
	
	// Update is called once per frame
	void Update () {
       Debug.Log(FindClosestTarget().name);
       transform.position = Vector3.MoveTowards(transform.position, FindClosestTarget().transform.position, speed * Time.deltaTime);
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
            if(curDistance < distance)
            {
                closest = gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }
}
