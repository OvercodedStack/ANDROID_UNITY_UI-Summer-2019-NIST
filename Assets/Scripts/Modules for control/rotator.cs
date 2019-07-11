/// A general class that is used as a method for rotating the end-effector
/// based on the use of visual reference points for a gameobject
/// Created on July 3, 2019 by Esteban Segarra 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour { 
    private Vector3 mOffset;
    private float mZCoord;

    //Find the reference point on the gameobject and set to it. 
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    //Convert the mouse pointer location from the screen to the virtual world
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public Vector3 debug_vector;

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 hitpoint;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        Debug.Log(ray);

        RaycastHit hit;

        hitpoint = this.transform.position;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
        { 
            hitpoint = new Vector3(hit.point.x, hit.transform.position.y + 1, hit.point.z);

        }


        Vector3 mouse_world_pos = GetMouseWorldPos();
        Vector3 new_pos;
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mZCoord);

        //Depending on what gameobject this item is applied, the behaviour is dependent on the 
        //object name applied. 
        switch (this.name)
        {
            case "Control_pt":
                transform.parent.position = mouse_world_pos + mOffset;
                break;
            case "Y_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.parent.position = new Vector3(transform.root.position.x, new_pos.y, transform.root.position.z);
                break;
            case "X_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.root.position = new Vector3(transform.root.position.x, transform.root.position.y, hitpoint.z);
                break;
            case "Z_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.root.position = new Vector3(new_pos.x, transform.root.position.y, transform.root.position.z);
                break;
            case "Y_rot_axis":
                //new_pos = new Vector3(0, (mouse_world_pos.y / Screen.height), 0);
                //transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            case "Z_rot_axis":
                //new_pos = new Vector3(0, 0, (mouse_world_pos.z / Screen.width) * 2);
                //transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            case "X_rot_axis":
                // new_pos = new Vector3((mouse_world_pos.x / Screen.width) * 2, 0, 0);
                // transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            default:
                //transform.parent.position = GetMouseWorldPos() + mOffset;
                break;
        }
    }
}

//distance = Vector3.Distance(this.transform.position,Camera.main.transform.position);
/*
Vector3 temp_pos = transform.position;
Vector3 mouse_pos = new Vector3(temp_pos.x +Input.mousePosition.x, temp_pos.y + Input.mousePosition.y, distance);
Vector3 obj_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
debug_vector = obj_pos;
transform.position = obj_pos; */
