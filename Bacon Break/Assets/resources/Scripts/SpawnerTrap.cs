using UnityEngine;
using System.Collections;

public class SpawnerTrap : MonoBehaviour
{
   public Vector3 thisPos;
    bool spawnNow = false;
    public GameObject myTrap;
    // Use this for initialization
    void Start()
    {
        Vector3 thisPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNow)
            myTrap.SetActive(true); ;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            thisPos.y = .02f;
            this.transform.position -= thisPos;
            spawnNow = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            thisPos.y = .02f;
            this.transform.position += thisPos;
        }
    }
}