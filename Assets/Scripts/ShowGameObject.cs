///////////////////////////////////////////////////////////////////////////////
//
//  Original System: ShowGameObject.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  This is a publically-accessible script that allows the user to change if a gameobject should be seen or not. 
//
//  Note: This script surpasses Activate UI on Button 
//
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameObject : MonoBehaviour {

    public GameObject item; // Assign in inspector
    public bool isShowing = false;

    public void hide_or_show()
    {
        isShowing = !isShowing;
        item.SetActive(isShowing);
    }
}
