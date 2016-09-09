using UnityEngine;
using System.Collections;

public class TapScreen : MonoBehaviour {

    bool newGame;
    GameObject pulse;
    public GameObject pulsingPrefab;
    Vector3 pos;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        newGame = true;
    }

    void Update()
    {
    //#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            pos.z = 10.5f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            if (newGame)
            {
                ray = new Ray(pos, Vector3.back);
                Debug.DrawRay(pos, Vector3.back, Color.green);
                Physics.Raycast(ray, out hit);
                //Debug.Log(hit.collider);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "newTarget")
                    {
                        foreach (Transform child in transform)
                        {
                            GameObject.Destroy(child.gameObject);
                        }

                        newGame = false;
                    }
                }
            }
            else
            {
                pulse = (GameObject)Instantiate(pulsingPrefab);
                pulse.transform.position = pos;
                pulse.transform.SetParent(transform);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "target")
                    {
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                }
        }
    /*#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);

            pos = Input.GetTouch(0).position;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);

            if (myTouch.phase == TouchPhase.Began)
            {
                if (newGame)
                {
                    foreach (Transform child in transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    newGame = false;
                }
                else
                {
                    pulse = (GameObject)Instantiate(pulsingPrefab);
                    pulse.transform.position = myTouch.position;
                    pulse.transform.SetParent(transform);
                }
                //Touch[] myTouches = Input.touches;
                //for (int i = 0; i < Input.touchCount; i++)
                //{

                //Do something with the touches
                //}
            }
        }
        #endif*/
    }
}