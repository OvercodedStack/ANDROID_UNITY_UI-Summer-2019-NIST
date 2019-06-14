using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class TCP_scanner_and_selector_19 : MonoBehaviour
{
    //The gameobjects unique to each other and setup using the respective gameobjects to locate them in the virtual space. 
    private GameObject UR5;
    private GameObject UR10L;
    private GameObject UR10R;
    private GameObject ABBL;
    private GameObject ABBR;
    public GameObject target_robot;

    private raycast_collision_19 ray_collider; // Script that is used to locate a raycast between objects: AKA the player vs what they stare at
    public string broadcast_IP;//Vicom IP address from which ports will connect to recieve data from. 
    public int[] robot_ports;  //Ports which contain the coordinates and labels of the robots in Vicom. 

    // Use this for initialization
    void Start()
    {
        //Setting up ops 
        ray_collider = GetComponent<raycast_collision_19>();
        UR5 = GameObject.Find("UR5");
        UR10L = GameObject.Find("UR10L");
        UR10R = GameObject.Find("UR10R");
        ABBL = GameObject.Find("ABBL");
        ABBR = GameObject.Find("ABBR");
        target_robot = ray_collider.get_obj();

        //tcp_scan(44032, 44039,broadcast_IP);
        track_game_objects();
        return_target_object();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //This is used to check what object is being stared by the player. 


    //Instead of creating new scanner, just check the ports and assign them to IDs beforehand 
    void track_game_objects()
    {
        TcpClient tcp = new TcpClient();
        foreach (int port_val in robot_ports)
        {
            try
            {
                tcp = new TcpClient(broadcast_IP, port_val);
                NetworkStream stream = tcp.GetStream();

                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                Byte[] data = new Byte[256];

                // String to store the response ASCII representation.
                string responseData = "";

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                ///Requires specialized format to decode from Vicon
                string[] spilt_words = responseData.Split(',');
                string type = spilt_words[0];
                int robot_id = decode_str(type); //The identifier ID for the robot on the designated port 

                Debug.Log("Done with recieving data");

                stream.Close();
                tcp.Close();
            }
            catch
            {
                continue;
            }

            finally
            {
                try
                {
                    tcp.Close();
                }
                catch
                {
                    Debug.Log("Error with closing tcp client");
                }
            }
        }
    }

    int decode_str(string word)
    {
        switch (word)
        {
            case "UR5":
                return 1;
            case "UR10L":
                return 2;
            case "UR10R":
                return 3;
            case "ABBL":
                return 4;
            case "ABBR":
                return 5;
            default:
                return 0;
        }
    }

    /// 0 or null will be designated as the case where there is no ID request position at all. 
    public int return_target_object()
    {
        //Uses the gameobject name as a means of determining which robot is being looked at with the Vicon 
        switch (target_robot.name)
        {
            case "UR5":
                return 1;
            case "UR10L":
                return 2;
            case "UR10R":
                return 3;
            case "ABBL":
                return 4;
            case "ABBR":
                return 5;
            default:
                return 0;
        }
    }

    void set_object_location(GameObject obj_item, float x, float y, float z)
    {
        obj_item.transform.position = new Vector3(x, y, z);
    }
}
