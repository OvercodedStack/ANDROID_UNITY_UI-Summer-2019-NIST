///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Mouse_drag.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_Open_VR V2
//  Revision:        1.0 - 6/22/2018
//                   2.0 - 7/23/2019                     
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Wrapper to move gameobjects in-game. This script is legacy and superceded by rotator.cs. 
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
    public InputField InputField_output_vector_position; 

    void Start()
    {
        //Startup position and rotation
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
        InputField_output_vector_position.text = transform.position.ToString(); 
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


    //When activated, this function simply overwrites the current position and rotation with the start-up ones. 
    public void reset_pos_butt()
    {
        transform.position = reset_position_vec;
        transform.rotation = reset_orientation; 
    }
}
