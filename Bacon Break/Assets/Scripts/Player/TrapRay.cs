using UnityEngine;
using System.Collections;

public class TrapRay : MonoBehaviour {


    //private RaycastHit[] hit;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    #if UNITY_EDITOR
            simpleControls();
        #else
            tapControls();
        #endif
	}

    void simpleControls()
    {
        
    }

    void tapControls()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                var hit = Physics.SphereCastAll(ray, 5f, 100f);

                foreach (RaycastHit temp in hit)
                {
                    if (temp.transform.gameObject.tag == "Trap" && temp.transform.GetComponent<TrapTap>() != null)
                    {
                        temp.transform.GetComponent<TrapTap>().OnTap();
                    }
                }
            }
        }
    }
}
