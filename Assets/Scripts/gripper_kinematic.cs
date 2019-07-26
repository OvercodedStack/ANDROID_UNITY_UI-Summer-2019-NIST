///////////////////////////////////////////////////////////////////////////////
//
//  Original System: gripper_kinematics.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_interpreter.cs
//  Revision:        1.0 - 7/5/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Gripper Control on UR5
//
///////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gripper_kinematic : MonoBehaviour {
    private GameObject gripper_left, gripper_right;
    private Vector3 manipulator_grip_L, manipulator_grip_R; 
    private float max_limit = 0.3f;
    private float min_limit = 0.075f;
    //private VIVE_controller controller;
    private float grip_ratio = 0;
    //public Slider bar;                                    //UNCOMMENT THIS LINE IN ORDER TO ENABLE THE SLIDER BAR OPTION 
    public GameObject img_open_gripper;
    public GameObject img_closed_gripper; 


    // Use this for initialization
    void Start () {
        gripper_left = GameObject.Find("Left Handle");
        gripper_right = GameObject.Find("Right Handle");
        img_closed_gripper.SetActive(false);

        //GameObject controller_device = GameObject.Find("Controller (left)");
        //controller = controller_device.GetComponent<VIVE_controller>();
    }

    public void set_grip(float val)
    {
        grip_ratio = val; 
    }

    //This function is to be toggled directly by a unity button as a click event in its settings. 
    //This function also allows changing if the image when the gripper changes 
    public void toggle_gripper_override()
    {
        if (grip_ratio == 0)
        {
            grip_ratio = 1;
            img_open_gripper.SetActive(false);
            img_closed_gripper.SetActive(true);
        }
        else
        {
            grip_ratio = 0;
            img_open_gripper.SetActive(true);
            img_closed_gripper.SetActive(false);
        }

    }


	// Update is called once per frame
	void Update () {
        //grip_ratio = bar.value;                   //UNCOMMENT THIS LINE IN ORDER TO ENABLE THE SLIDER BAR OPTION 
        
        //Set a float that goes from 0.000 - 1.000
         update_grippers(grip_ratio);
	}

    public float get_ratio()
    {
        return grip_ratio;
    }

    void update_grippers(float close_ratio)
    {
        float my_grip_ratio = 0.3f - (0.225f * close_ratio);
        manipulator_grip_L = new Vector3(0, 2, my_grip_ratio);
        manipulator_grip_R = new Vector3(0, 2, -my_grip_ratio);
        gripper_left.transform.localPosition = manipulator_grip_L;
        gripper_right.transform.localPosition = manipulator_grip_R;

    }
}
