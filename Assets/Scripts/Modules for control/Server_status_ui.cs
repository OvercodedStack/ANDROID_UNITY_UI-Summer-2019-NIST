using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPC_Server;

/// <summary>
/// The purpose of this script is nothing more than to show the status of the server on a bar below. It should theorically determine if the server has a connected client or not. 
/// </summary>


public class Server_status_ui : MonoBehaviour {
    public Image img;
    public TCP_Server Server;
    private bool status;
    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
        img.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        status = Server.server_status;
    }
	
	// Update is called once per frame
	void Update () {
        if (status)
        {
            img.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        }
        else
        {
            img.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        }
    }
}
