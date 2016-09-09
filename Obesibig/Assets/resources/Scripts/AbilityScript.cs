using UnityEngine;
using System.Collections;

public class AbilityScript : MonoBehaviour {

    public GameObject superCellPrefab;

    public void SuperCellButton()
    {
        Instantiate(superCellPrefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4.5f, 4.5f), 0), Quaternion.identity);
    }
}
