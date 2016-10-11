using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraDrag : MonoBehaviour
{
    public float speed = 0.5f;
    public GameObject worldBB;
    public GameObject player;
    public GameObject pnl_level;
    public Button btn_refocus;

    //For mouse
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Vector2 origin;
    private float leftSide, rightSide, topSide, bottomSide;
    float height, width;

    // Use this for initialization
    void Start()
    {
        leftSide = worldBB.GetComponent<BoxCollider>().bounds.min.x;
        rightSide = worldBB.GetComponent<BoxCollider>().bounds.max.x;
        topSide = worldBB.GetComponent<BoxCollider>().bounds.max.z;
        bottomSide = worldBB.GetComponent<BoxCollider>().bounds.min.z;

        height = 2.0f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        btn_refocus.onClick.AddListener(RefocusCamera);

        RefocusCamera();
    }

    // Update is called once per frame
    void Update()
    {
        //Drag camera with touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x * -speed, 0, touchDeltaPosition.y * -speed, Space.World);

            //Clamp camera so it won't go over the edges of the map
            ClampCamera();
        }

        //Drag camera with mouse (for debug purposes)
         /*if (Input.GetMouseButtonDown(0))
         {
             dragOrigin = Input.mousePosition;
             return;
         }

         if (!Input.GetMouseButton(0)) return;

         Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
         Vector3 move = new Vector3(pos.x * -dragSpeed, 0, pos.y * -dragSpeed);

         transform.Translate(move, Space.World);
         ClampCamera();
         */
    }

    public void RefocusCamera()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        ClampCamera();
    }

    void ClampCamera()
    {
        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftSide + (width / 2), rightSide - (width / 2)),
                transform.position.y,
                Mathf.Clamp(transform.position.z, bottomSide + (height / 2), topSide - (height / 2)));
    }


}
