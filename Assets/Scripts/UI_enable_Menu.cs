///////////////////////////////////////////////////////////////////////////////
//
//  Original System: UI_enable_Menu.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/3/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This is a very simple script to activate or deactive UI elements. This works simply by enabling and disabling the gameobject thus giving the 
//  illusion that it's being hidden or shown. 
//  To apply for the use of on a UI, simply place this component onto a gameobject, use a button OnClick() event, and find the public function "hide or show()" 
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
 

public class MenuAppearScript : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void hide_or_show()
    {
        isShowing = !isShowing;
        menu.SetActive(isShowing);
    }
}
