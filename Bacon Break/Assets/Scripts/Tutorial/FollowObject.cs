﻿using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
    public GameObject followObject;	
	// Update is called once per frame
	void Update () {
        this.transform.position = followObject.transform.position;
	}
}
