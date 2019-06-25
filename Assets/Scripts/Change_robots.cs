using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPC_Server;
using UnityEngine.UI;
 
public class Change_robots : MonoBehaviour {
    public bool change_robot;
    TCP_Server server;
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
 
        server = GetComponent<TCP_Server>();
        selected_robot = "None";
        change_robot = false;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        output_text.text = selected_robot;
       
    }

    void Update()
    {
        if (!bypass_vic.isOn)
        {
            float now_time = Time.time;
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
            selected_robot = rbt_list[selc_value.value];

        }
    }

    public void change_state()
    {
        change_robot = !change_robot;
        old_time = Time.time;
        image.GetComponent<Image>().color = new Color32(0, 255, 0, 100);

    }
}
