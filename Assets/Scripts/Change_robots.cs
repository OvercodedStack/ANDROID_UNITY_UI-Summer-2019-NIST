///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Camera_controls.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Simple script that allows the changing of the selected robot in the android application. Utilizes data adquired from the TCP server
//  and displays the currently selected robot in the UI.
//
///////////////////////////////////////////////////////////////////////////////
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPC_Server;
using UnityEngine.UI;
 
public class Change_robots : MonoBehaviour {
    public bool change_robot;
    TCP_Server server;
    UR5_to_TPC phraser;
    float old_time;
    public Image image;
    public string selected_robot;
    public Text output_text;
    public raycast_collision_19 ray_caster;
    public Toggle bypass_vic;
    private int selection;
    public Dropdown selc_value;
    string[] rbt_list = new string[6] {"None", "UR5", "UR10L", "UR10R", "ABBL", "ABBR"};

    const int TIMEOUT_PERIOD_FOR_ROBOT_SELECTION = 1; 

    // Use this for initialization
    void Start() {
        phraser = GetComponent<UR5_to_TPC>();
        server  = GetComponent<TCP_Server>();
        selected_robot = "No robot selected";
        change_robot = false;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(210, 139, 9, 100);//The warning indicator
        output_text.text = selected_robot;
       
    }

    void Update()
    {

        //Using this variable to tell the phraser (CRPI) to change the currently selected robot. 
        if (!bypass_vic.isOn)
        {
            float now_time = Time.time;
            phraser.change_robots_bool = change_robot;
            if (now_time > old_time + TIMEOUT_PERIOD_FOR_ROBOT_SELECTION && change_robot)
            {
                change_robot = !change_robot;
                image.GetComponent<Image>().color = new Color32(210, 139, 9, 100); //The Green indicator
            }
            else if (change_robot)
            {
                try
                {
                    //Depending on the object the ray hits, the name of that object is taken 
                    selected_robot = ray_caster.get_name();

                }
                catch
                {
                    selected_robot = "None";
                }
                //Output the results
                output_text.text = selected_robot;
            }
        }
        else
        {
            output_text.text = server.get_msg();
            selected_robot = rbt_list[selc_value.value];
            phraser.change_robots_bool = true;
        }
    }


    //UI-specific, shows when the timeout period is done. 
    public void change_state()
    {
        change_robot = !change_robot;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(4, 113, 13, 100); //The  indicator

    }
}
