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
    public float additive_ratio = 1.0f;
    private Vector3 reset_position_vec;
    private Quaternion reset_orientation;
    public Toggle move_to_position;
    public GameObject indicator;
    public float speed = 3.0F;

    public Button plus_but;
    public Button min_but;

    void Start()
    {
        reset_position_vec = transform.position;
        reset_orientation = transform.rotation;
    }


    void Update()
    {

        if (move_to_position.isOn)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position,indicator.transform.position,step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,indicator.transform.rotation,step);
        }
    }


    private void OnMouseDown()
    {
        distance = Vector3.Distance(this.transform.position,Camera.main.transform.position);
    }


    public Vector3 debug_vector;
    private void OnMouseDrag()
    {
        Vector3 temp_pos = transform.position;

        Vector3 mouse_pos = new Vector3(temp_pos.x +Input.mousePosition.x, temp_pos.y + Input.mousePosition.y, distance);
        Vector3 obj_pos = Camera.main.ScreenToWorldPoint(mouse_pos);

        debug_vector = obj_pos;

        transform.position = obj_pos; 
    }

    //Put item further away
    public void plus_button()
    {
        distance += additive_ratio;
    }

    //Put item closer away;
    public void sub_button()
    {
        distance += -additive_ratio;
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
