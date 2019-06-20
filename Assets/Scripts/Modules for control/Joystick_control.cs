using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick_control : MonoBehaviour {
    public LeftJoystick l_joy;
    public RightJoystick r_joy;
    Button button;
    bool invert_axis;
    public Slider spd_drg; 
 

 	void LateUpdate () {
        Vector3 angls = transform.rotation.eulerAngles;
        Vector3 l_vec = l_joy.GetInputDirection();
        Vector3 r_vec = r_joy.GetInputDirection();
        l_vec = l_vec * spd_drg.value;
        r_vec = r_vec * spd_drg.value;

        if (invert_axis)
        {
            transform.Rotate(l_vec.x, l_vec.y, 0);
            transform.Translate(r_vec.x, r_vec.y, 0);
        }
        else
        {
            transform.Rotate(0, l_vec.x, l_vec.y);
            transform.Translate(0, r_vec.x, r_vec.y);
        }
	}

    public void on_action()
    {
        invert_axis = !invert_axis;
    }
}
