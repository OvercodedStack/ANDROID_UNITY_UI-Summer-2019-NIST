///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Robot_data_utilities.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Unity workspace?
//  Revision:        1.0 - 6/13/19
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Data interpreter for all robot utilities and status such as the DO ports, robot ID, and 
//  gripper status. 
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_data_utilities : MonoBehaviour {
    Change_robots chgner;                   //Contains the data required for determining robot type
    public string data_out;                 //Exports fully interpreted data in the following format
                                            // {Robot ID, gripper status, DO1, DO2, etc} all in float
    gripper_kinematic grip_status;          //It's a float variable between 0 and 1; 
    public bool[] Digital_out = new bool[4];// ????? 

    // Use this for initialization
	void Start () {
        data_out = " "; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
