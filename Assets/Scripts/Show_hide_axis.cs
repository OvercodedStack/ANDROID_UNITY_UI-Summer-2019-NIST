///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Show_hide_axis.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/5/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This script determines which mode should the end-piece manipulator should be active at a given time.  
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Show_hide_axis : MonoBehaviour {
    public GameObject axis_x;
    public GameObject axis_y;
    public GameObject axis_z;

    public GameObject axis_rot_x;
    public GameObject axis_rot_y;
    public GameObject axis_rot_z;

    public GameObject cursr; 
    int mode = 0; 

    void Start()
    {
        //Default to this "axis mode" 
        axis_rot_x.SetActive(false);
        axis_rot_y.SetActive(false);
        axis_rot_z.SetActive(false);
        cursr.SetActive(false);
    }

    public void swap_modes()
    {
        mode++;
        switch (mode)
        {
            //Show the XYZ axis
            case 0:
                axis_x.SetActive(false);
                axis_y.SetActive(false);
                axis_z.SetActive(false);
                break;
            //Show the Rotator loops
            case 1:
                axis_rot_x.SetActive(true);
                axis_rot_y.SetActive(true);
                axis_rot_z.SetActive(true);
                axis_x.SetActive(false);
                axis_y.SetActive(false);
                axis_z.SetActive(false);
                break;
            //Show "freemode" box to move dynamically
            case 2:
                axis_rot_x.SetActive(false);
                axis_rot_y.SetActive(false);
                axis_rot_z.SetActive(false);
                cursr.SetActive(true);
                break;
            default:
                axis_x.SetActive(true);
                axis_y.SetActive(true);
                axis_z.SetActive(true);
                cursr.SetActive(false);
                mode = 0; 
                break;
        }   
    }
}
