//This script is an alternative version of a second script which handles the displaying of a gameobject
//In this script, UIs can be shown or hidden using a button OnClick() event. 

//This script is legacy and is supersceded by ShowGameObject.cs

//Author: Esteban Segarra 
//Date: Jul 31, 2019 
//Version: 1.0 


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_enable_Menu : MonoBehaviour
{

    public GameObject menu;         // Assign in unity editor inspector
    private bool isShowing = false; // Enables or disables the gameobject class 

    private void Start()
    {
        menu.SetActive(isShowing);
    }

    public void hide_or_show()
    {
        isShowing = !isShowing;
        menu.SetActive(isShowing);
    }
}
