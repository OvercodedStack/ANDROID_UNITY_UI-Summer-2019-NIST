using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This class allows the user to rotate the end effector with the leftmost joystick and move the endeffector with the rightmost joystick
//Pressing a button allows a transformation of the axis of change. 
public class Joystick_control : MonoBehaviour {
    public LeftJoystick l_joy;
    public RightJoystick r_joy;
    Button button;
    bool invert_axis;
    public Slider spd_drg;
    private float fine_adjustment = 0.15F; 
    public Image XZ_translation;
    public Image Y_translation;
 

 	void LateUpdate () {
        Vector3 angls = transform.rotation.eulerAngles;
        Vector3 l_vec = l_joy.GetInputDirection();
        Vector3 r_vec = r_joy.GetInputDirection();
        l_vec = l_vec * spd_drg.value;
        r_vec = r_vec * spd_drg.value;

        if (invert_axis)
        {
            transform.Rotate(l_vec.x, 0, l_vec.y);
            transform.Translate(r_vec.x * fine_adjustment,0, r_vec.y * fine_adjustment);
            XZ_translation.gameObject.SetActive(true);
            Y_translation.gameObject.SetActive(false);
        }
        else
        {
            transform.Rotate(0,l_vec.y,0);
            transform.Translate(0, r_vec.y * fine_adjustment,0);
            Y_translation.gameObject.SetActive(true);
            XZ_translation.gameObject.SetActive(false);
        }
	}

    public void on_action()
    {
        invert_axis = !invert_axis;
    }
}
