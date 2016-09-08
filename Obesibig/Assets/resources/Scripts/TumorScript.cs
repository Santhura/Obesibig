using UnityEngine;
using System.Collections;

public class TumorScript : MonoBehaviour {
    
    private float speed =  1f /1000f;
    private float startDampPoint = 1.25f;
    private bool growTumor = true;
    private bool stopGrowth = true;
    private Vector3 increase;

	// Use this for initialization
	void Start () {
        increase = new Vector3(speed, speed, 0);
	}
	
	// Update is called once per frame
	void Update () {

        CreateCancerCell();

        CheckTumorSize();

	}

    public void CheckTumorSize()
    {
        //Grow the tumor
        if (growTumor && Mathf.Round(this.gameObject.transform.localScale.x * 100f) / 100f >= 0.0f &&
        Mathf.Round(this.gameObject.transform.localScale.x * 100f) / 100f <= startDampPoint)
        {
            IncreaseTumor();
        }

        //Damp the growth
        if (Mathf.Round(this.gameObject.transform.localScale.x * 100f) / 100f > startDampPoint &&
        Mathf.Round(this.gameObject.transform.localScale.x * 100f) / 100f >= 0.0f)
        {
            //start damping the increasing tumor speed
            //growTumor = false;
            //DecreaseTumor();
            //DampTumor();
            
        }
    }

    public void IncreaseTumor()
    {
        this.gameObject.transform.localScale -= increase;
    }

    public void DecreaseTumor()
    {
        this.gameObject.transform.localScale -= increase;
    }

    public void DampTumor()
    {
        speed = speed - 0.0001f;
        increase = new Vector3(speed, speed, 0);
        this.gameObject.transform.localScale += increase;

        Debug.Log("speed of damp " + speed);
    }

    public void CreateCancerCell()
    {
        //The bigger the size of the tumor the more cells it will create.

    }
}
