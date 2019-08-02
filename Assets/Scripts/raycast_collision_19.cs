///////////////////////////////////////////////////////////////////////////////
//
//  Original System: raycast_collision_19.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 6/15/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  The purpose of this script is to be able to detect another gameobject located in the virtual space with a raytrace. 
//  Both the raycast and the recieving object should have colliders that can be utilized for detecting when one object has touched the other. 
//
///////////////////////////////////////////////////////////////////////////////
 
using UnityEngine;

public class raycast_collision_19 : MonoBehaviour {

    public float distance_ray;
    private LineRenderer ir_laser;
    public GameObject hit_obj; 

	// Use this for initialization
	void Start () {
        ir_laser = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit ray;
        if (Physics.Raycast(transform.position, transform.forward, out ray, distance_ray))
        {
            hit_obj = ray.transform.gameObject;

            ir_laser.SetPosition(1, new Vector3(0,0,ray.distance)); 
        }
        else
        { 
            ir_laser.SetPosition(1, new Vector3(0, 0, distance_ray));
        }
	}

    //Simple method to get what was hit for use with other scripts. 
    public GameObject get_obj()
    {
        return hit_obj;
    }

    public string get_name()
    {
        return hit_obj.name;
    }
}
