using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick_control : MonoBehaviour {
    public LeftJoystick l_joy;
    public RightJoystick r_joy;
    Button button;
    bool invert_axis;
    float scaling_number = 1.0F;
    public Vector3 pub;


 	void LateUpdate () {
        Vector3 angls = transform.rotation.eulerAngles;
        Vector3 l_vec = l_joy.GetInputDirection();
        Vector3 r_vec = r_joy.GetInputDirection();
        pub = new Vector3(l_vec.x, l_vec.y, angls.z);
        transform.Rotate(l_vec.x, l_vec.y, 0);

        transform.Translate(r_vec.x, r_vec.y, 0);
	}

    void on_action()
    {
        invert_axis = !invert_axis;
    }
}
