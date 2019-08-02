///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Camera_orbit_around_model.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This script is legacy. This was meant for use on a gameobject to be kept rigid at a specifide location.  
//
///////////////////////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Camera_orbit_around_model : MonoBehaviour {
    GameObject anchor;
    //public Toggle UI_Button;
    public string speed; 
	// Use this for initialization
	void Start () {
        anchor = GetComponent<GameObject>();
        this.transform.eulerAngles = new Vector3(43, -30, 0);
	}
	
	// Update is called once per frame
	void Update () {

        speed = Input.mouseScrollDelta.ToString(); 
        /*
		if (Input.GetButton("Fire1"))
        {
            Input.acceleration
            anchor.transform.rotation.eulerAngles = 
        }*/
	}
}
