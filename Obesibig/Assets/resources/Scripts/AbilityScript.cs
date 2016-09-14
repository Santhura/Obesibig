using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AbilityScript : MonoBehaviour {

    public GameObject superCellPrefab;

    float timer = 1;
    public void SuperCellButton()
    {
        Instantiate(superCellPrefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4.5f, 4.5f), 0), Quaternion.identity);
    }

    void Update()
    {
        if(GameObject.Find("A Tumor") == null)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
