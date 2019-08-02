///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Switch_scenes.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This script switches the color of a blank UnityEngine.UI.Image class to a different color.  
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI; 

public class UI_Image_indicator : MonoBehaviour {
    private bool status;
    public Image img;

    public void toggle_status()
    {
        status = !status;
    }
    void Update()
    {
        if (status)
        {
            img.GetComponent<Image>().color = new Color32(4, 113, 13, 100); //The warning indicator
        }
        else
        {
            img.GetComponent<Image>().color = new Color32(210, 139, 9, 100); //The green Indicator
        }
    }
}
