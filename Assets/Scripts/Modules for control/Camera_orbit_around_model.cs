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
