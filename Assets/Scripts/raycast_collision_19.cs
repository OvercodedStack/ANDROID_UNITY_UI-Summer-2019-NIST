using System.Collections;
using System.Collections.Generic;
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
