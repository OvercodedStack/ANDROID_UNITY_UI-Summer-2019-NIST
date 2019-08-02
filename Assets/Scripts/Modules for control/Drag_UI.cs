///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Drag_UI.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This script allows a menu item to be moved around the screen without the use of a additional scripts. 
//
//  To use: 
//          Add this script to a game object 
//          Add a Event Trigger component to the gameobject
//          Create a Begin Drag event and a Drag event in the Event Trigger component
//          Drag this gameobject into the specified boxes
//          Bind those events with the functions BeginDrag and OnDrag provided in this script. 
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;

//Referenced https://www.youtube.com/watch?v=hzuxb8CPGyQ
public class Drag_UI : MonoBehaviour
{
    private float offsetX;
    private float offsetY;

    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
}