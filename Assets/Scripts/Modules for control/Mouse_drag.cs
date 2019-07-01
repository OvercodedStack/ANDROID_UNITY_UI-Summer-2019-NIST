///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Mouse_drag.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_Open_VR V2
//  Revision:        1.0 - 6/22/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Wrapper to move gameobjects in-game
//
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_drag : MonoBehaviour {
    public float distance;
    public float additive_ratio = 30.0f;
    private Vector3 reset_position_vec;
    private Quaternion reset_orientation;
    public Toggle move_to_position;
    public GameObject indicator;
    public float speed = 3.0F;

    void Start()
    {
        reset_position_vec = transform.position;
        reset_orientation = transform.rotation;
    }


    void Update()
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            selected = hit.transform.gameObject;
        }*/
        if (move_to_position.isOn)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position,indicator.transform.position,step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,indicator.transform.rotation,step);
        }
    }



    //Put item further away
    public void plus_button()
    {
        //mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //distance += additive_ratio;

        transform.position = transform.position + (Camera.main.transform.forward * 0.75F);
    }

    //Put item closer away;
    public void sub_button()
    {
        //distance += -additive_ratio;
        transform.position = transform.position - (Camera.main.transform.forward * 0.75F);
    }

    void LateUpdate()
    {
        if (Input.GetButton("Q"))
        {
            distance += additive_ratio;
        }
        if (Input.GetButton("E"))
        {
            distance += -additive_ratio;
        }
    }

    public void reset_pos_butt()
    {
        transform.position = reset_position_vec;
        transform.rotation = reset_orientation; 
    }
}
