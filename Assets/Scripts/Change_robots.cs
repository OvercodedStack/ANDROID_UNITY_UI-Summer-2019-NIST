/*
Simple script that allows the changing of the selected robot in the android application. Utilizes data adquired from the TCP server
and displays the currently selected robot in the UI. 

7/11/19 - Esteban Segarra M. 
*/

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

    // Use this for initialization
    void Start() {
        phraser = GetComponent<UR5_to_TPC>();
        server  = GetComponent<TCP_Server>();
        selected_robot = "None";
        change_robot = false;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        output_text.text = selected_robot;
       
    }

    void Update()
    {

        //Using this variable to tell the phraser (CRPI) to change the currently selected robot. 
        if (!bypass_vic.isOn)
        {
            float now_time = Time.time;
            phraser.change_robots_bool = change_robot;
            if (now_time > old_time + 5 && change_robot)
            {
                change_robot = !change_robot;
                image.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            else if (change_robot)
            {
                try
                {
                    selected_robot = ray_caster.get_name();

                }
                catch
                {
                    selected_robot = "None";
                }
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

    public void change_state()
    {
        change_robot = !change_robot;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(0, 255, 0, 100);

    }
}
