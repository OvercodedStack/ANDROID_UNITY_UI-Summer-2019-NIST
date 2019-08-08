///////////////////////////////////////////////////////////////////////////////
//
//  Original System: UR5_to_TPC.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Unity workspace
//  Revision:        2.0 - 7/2/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Data phraser from UR5 to TPC server. It is handling the parts of the robot that will ultimately pass through CRPI for export. 
//
///////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using UnityEngine.UI;
using TPC_Server;
using System.Collections.Generic;

public class UR5_to_TPC : MonoBehaviour
{
    public string output_string;//Exports fully interpreted data in the following format
                                // {Angles};{Robot ID, gripper status, DO1, DO2, D03,D04, Bypass}; all in float
    TCP_Server server;
    ur5_kinematics angle_controller;
    public gripper_kinematic grip_obj;
    public GameObject robot;
    public Button send_msg;
    public bool enable_tcp_srv = true;
    public Toggle manual_bypass;
    public bool change_robots_bool;

    //========Options for the digital out dropdown menu
    public Dropdown digital_out_menu;
    List<string> string_list = new List<string>() {"Gripper", "Lamp Status" , "Digital out 3" , "Digital out 4" };
    private string current_selected;
    public Image indicator; 
    Change_robots chgner;                   //Contains the data required for determining robot type
    
    gripper_kinematic grip_status;          //It's a float variable between 0 and 1; 
    public bool DO1 = false;
    public bool DO2 = false;
    public bool DO3 = false;
    public bool DO4 = false;

    /// Determine best approach to leading the data from the digital out port to the output string.

    // Use this for initialization
    void Start()
    {
        server = GetComponent<TCP_Server>();
        GameObject gripper = GameObject.Find("Base_Gripper");
        //tcp_scan = GetComponent<TCP_scanner_and_selector_19>(); 
        grip_obj = gripper.GetComponent<gripper_kinematic>();
        output_string = "";
        chgner = GetComponent<Change_robots>(); 
        //GameObject robot = GameObject.Find("UR5");
        angle_controller = robot.GetComponent<ur5_kinematics>();
        send_msg.onClick.AddListener(add_active_state);

        //Digital Menu for choosing which ones shall be active and inactive. 
        digital_out_menu.ClearOptions();
        digital_out_menu.AddOptions(string_list);
    }

    //Given the digital input ports found on the dropdown menu on UI, show what potential options there are and input them here.
    //TODO: This script should be made dynamic to allow a larger and appendable amount of digital port IO 
    void Update()
    {
        bool temp_bool = false;
        current_selected = string_list[digital_out_menu.value];

        if (enable_tcp_srv)
            add_active_state();

        switch (current_selected)
        {
            case "Gripper":
                temp_bool = DO1;
                break;
            case "Lamp Status":
                temp_bool = DO2;
                break;
            case "Digital out 3":
                temp_bool = DO3;
                break;
            case "Digital out 4":
                temp_bool = DO4;
                break;
        }

        if (temp_bool)
        {
            indicator.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        }
        else
        {
            indicator.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        }
    }

    //This is a public method for a button to use. USE THIS INSTEAD OF IMPORTING A BUTTON OBJECT
    public void toggle_DO_button()
    {
        bool temp_bool;
        switch (current_selected)
        {
            case "Gripper":
                DO1 = !DO1;
                temp_bool = DO1;
                break;
            case "Lamp Status":
                DO2 = !DO2;
                temp_bool = DO2;
                break;
            case "Digital out 3":
                DO3 = !DO3;
                temp_bool = DO3;
                break;
            case "Digital out 4":
                DO4 = !DO4;
                temp_bool = DO4;
                break;
        }
    }

    //Adquires the data from the gripper
    string add_gripper()
    {
        string out_stg = null;
        out_stg += grip_obj.get_ratio().ToString();
        out_stg += ",";
        return out_stg;
    }

    //Converts the digital output ports into strings
    string convert_booleans()
    {
        string digital_out = "";
        digital_out += DO1 ? "1," : "0,";
        digital_out += DO2 ? "1," : "0,";
        digital_out += DO3 ? "1," : "0,";
        digital_out += DO4 ? "1" : "0";
        return digital_out;
    }

    //Merges all nessesary data from the robot to output to CRPI
    void add_active_state()
    {
        //String format for outputting to the tcp server.
        //{$UR5_pos:(value 1),(value 2),(value 3),(value 4),(value 5),(value 6), Robot Utilities:(Robot ID),(Gripper),(Digital Port 1),(Digital Port 2),(Digital Port 3),(Digital Port 4),
        //(Manual Bypass flag),(Vicon Robot changer flag)#} 

        output_string = convert_array(angle_controller.get_vector_UR5());   //Get the robot coordninates
        output_string += "Robot Utilities:";
        output_string += decode_str(chgner.selected_robot);                 //Convert robot ID to known 
        output_string += add_gripper();                                     //Get the gripper status
        output_string += convert_booleans();                                //Get Digital output feedback
        output_string += manual_bypass.isOn ? ",1" : ",0";                    //Choose to use Vicon or not
        output_string += change_robots_bool ? ",1" : ",0";
        output_string += ";#\n";


        server.set_msg(output_string);
    }


    //Publically accessible method that returns the final constructed message to CRPI
    public string get_message()
    {
        return output_string;
    }

    //Determines from the use of the name of the Robot, the appropiate Robot_ID#
    string decode_str(string word)
    {
        switch (word)
        {
            case "UR5":
                return "1,";
            case "UR10L":
                return "2,";
            case "UR10R":
                return "3,";
            case "ABBL":
                return "4,";
            case "ABBR":
                return "5,";
            default:
                return "0,";
        }
    }

    //Converts a joint array into a single string
    string convert_array(float[] array_in)
    {
        string output_str = null;
        output_str += "$UR5_pos:";
        for (int i = 0; i < 6; i++)
        {
            output_str += array_in[i].ToString();
            if (i < 5)
                output_str += ",";
        }
        output_str += ";";
        return output_str;
    }
}