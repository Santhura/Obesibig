using UnityEngine;
using System.Collections;

public class TrapRay : MonoBehaviour {


    public bool noTrapsTouched;
    public float tapSize = 3;

    public static bool hasPressed;

    // Use this for initialization
    void Start () {
        noTrapsTouched = true;
        hasPressed = false;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics.SphereCastAll(ray, tapSize, 1000f);

            foreach (RaycastHit temp in hit)
            {
                if (temp.transform.GetComponent<TrapTap>() != null && !temp.transform.GetComponent<TrapTap>().activated)
                {
                    temp.transform.GetComponent<TrapTap>().Tapped();
                }
                else if (temp.transform.GetComponent<StopTrapAnimation>() != null)
                {
                    temp.transform.GetComponent<StopTrapAnimation>().Tapped();
                    hasPressed = true;
                }
                else if (temp.transform.GetComponent<BridgeScript>() != null && !temp.transform.GetComponent<BridgeScript>().activated)
                {
                    temp.transform.GetComponent<BridgeScript>().Tapped();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
            hasPressed = false;
    }

    void tapControls()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                var hit = Physics.SphereCastAll(ray, tapSize, 1000f);

                foreach (RaycastHit temp in hit)
                {
                    if (temp.transform.GetComponent<TrapTap>() != null && !temp.transform.GetComponent<TrapTap>().activated)
                    {
                        temp.transform.GetComponent<TrapTap>().Tapped();
                        noTrapsTouched = false;
                    }
                    else if (temp.transform.GetComponent<StopTrapAnimation>() != null && !temp.transform.GetComponent<StopTrapAnimation>().activated)
                    {
                        temp.transform.GetComponent<StopTrapAnimation>().Tapped();
                        noTrapsTouched = false;
                        hasPressed = true;
                    }
                    else if (temp.transform.GetComponent<BridgeScript>() != null && !temp.transform.GetComponent<BridgeScript>().activated)
                    {
                        temp.transform.GetComponent<BridgeScript>().Tapped();
                        noTrapsTouched = false;
                    }
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                noTrapsTouched = true;
                hasPressed = false;
            }
        }
    }
}
