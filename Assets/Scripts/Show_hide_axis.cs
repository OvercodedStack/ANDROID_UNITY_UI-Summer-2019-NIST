///  Simple script to show or hide certain components regarding the 
///  endpoint manipulator. 
///  Developed on July 5th, 2019

using UnityEngine;

public class Show_hide_axis : MonoBehaviour {
    public GameObject axis_x;
    public GameObject axis_y;
    public GameObject axis_z;

    public GameObject axis_rot_x;
    public GameObject axis_rot_y;
    public GameObject axis_rot_z;

    public GameObject cursr; 
    int mode = 0; 

    void Start()
    {
        axis_rot_x.SetActive(false);
        axis_rot_y.SetActive(false);
        axis_rot_z.SetActive(false);
        cursr.SetActive(false);
    }

    public void swap_modes()
    {
        mode++;
        switch (mode)
        {
            case 0:
                axis_x.SetActive(false);
                axis_y.SetActive(false);
                axis_z.SetActive(false);
                break;
            case 1:
                axis_rot_x.SetActive(true);
                axis_rot_y.SetActive(true);
                axis_rot_z.SetActive(true);
                axis_x.SetActive(false);
                axis_y.SetActive(false);
                axis_z.SetActive(false);
                break;
            case 2:
                axis_rot_x.SetActive(false);
                axis_rot_y.SetActive(false);
                axis_rot_z.SetActive(false);
                cursr.SetActive(true);
                break;
            default:
                axis_x.SetActive(true);
                axis_y.SetActive(true);
                axis_z.SetActive(true);
                cursr.SetActive(false);
                mode = 0; 
                break;
        }   
    }
}
