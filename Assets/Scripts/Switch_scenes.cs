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
//  This script switches between scenes in the main menu of the app. 
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;
public class Switch_scenes : MonoBehaviour {
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GotoTest1()
    {
        SceneManager.LoadScene("Test Scenario 1");
    }
    public void GotoTest2()
    {
        SceneManager.LoadScene("Test Scenario 2");
    }
    public void GotoTest3()
    {
        SceneManager.LoadScene("Test Scenario 3");
    }

}
