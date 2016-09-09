using UnityEngine;
using System.Collections;

public class TumorScript : MonoBehaviour {
    
    private float speed =  100;
    private int tumorSize = 1;
    private int startDampPoint = 125;
    private bool growTumor = true;
    private bool startDamp = false;
    private bool stopGrowth = true;

    public int tumorHealth = 100;

    private Vector3 increase;

    public GameObject Cellprefab;
    public GameObject OrganParticlePrefab;

	// Use this for initialization
	void Start () {
        speed = speed / 100000f;
        increase = new Vector3(speed, speed, 0);
        CreateCancerCell();
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log("The size of tumor is " + tumorSize);

        CheckTumorSize();
        
        //Kill tumor
        if (tumorHealth <= 0)
        {
            Destroy(gameObject);
        }

        //Tumor wins
        if (gameObject.transform.localScale.x > 2f)
        {
            Instantiate(OrganParticlePrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}

    public void CheckTumorSize()
    {
        //Grow the tumor
        if (tumorSize >= 1 && growTumor)
        {
            IncreaseTumor();
            tumorSize++;
        }

        //Checks if damping needs to start
        if (tumorSize >= startDampPoint)
        {
            growTumor = false;
            startDamp = true;
            
            //DecreaseTumor();
        }

        //start slowing down the growth state
        if (startDamp)
        {
            Debug.Log("I am starting the damping now");
            tumorSize--;
            DampTumor();
        }

        //switch from damping to other state
        if (!growTumor && (tumorSize == 105))
        {
            growTumor = true;
            //startDamp = false;
        }
    }

    public void IncreaseTumor()
    {
        this.gameObject.transform.localScale += increase;
    }

    public void DecreaseTumor()
    {
        this.gameObject.transform.localScale -= increase;
    }

    public void DampTumor()
    {
        //speed = speed - 1f / 10000f;
        speed = speed * -1f;

        increase = new Vector3(speed, speed, 0);
        this.gameObject.transform.localScale += increase;

        Debug.Log("speed of damp " + speed);

        if (tumorSize == 105)
        {
            startDamp = false;
            speed = 100f / 100000f;
        }
    }

    public void CreateCancerCell()
    {
        //The bigger the size of the tumor the more cells it will create.
        StartCoroutine(MakeCell(5.0F));
        
    }

    IEnumerator MakeCell(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(Cellprefab, gameObject.transform.position, Quaternion.identity);
    }

}
