using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {

    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        //distance = Vector3.Distance(this.transform.position,Camera.main.transform.position);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();


    }

 
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public Vector3 debug_vector;
    private void OnMouseDrag()
    {
        /*
        Vector3 temp_pos = transform.position;
        Vector3 mouse_pos = new Vector3(temp_pos.x +Input.mousePosition.x, temp_pos.y + Input.mousePosition.y, distance);
        Vector3 obj_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
        debug_vector = obj_pos;
        transform.position = obj_pos; */
        Vector3 mouse_world_pos = GetMouseWorldPos();
        Vector3 new_pos;
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mZCoord);

        switch (this.name)
        {
            case "Control_pt":
                transform.parent.position = mouse_world_pos + mOffset;
                break;
            case "Y_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.parent.position = new Vector3(transform.position.x, new_pos.y, transform.position.z);
                break;
            case "X_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.parent.position = new Vector3(transform.position.x, transform.position.y, new_pos.y);
                break;
            case "Z_axis":
                new_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
                transform.parent.position = new Vector3(new_pos.y, transform.position.y, transform.position.z);
                break;
            case "Y_rot_axis":
                new_pos = new Vector3(0, (mouse_world_pos.y / Screen.height), 0);
                transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            case "Z_rot_axis":
                new_pos = new Vector3(0, 0, (mouse_world_pos.z / Screen.width) * 2);
                transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            case "X_rot_axis":
                new_pos = new Vector3((mouse_world_pos.x / Screen.width) * 2, 0, 0);
                transform.parent.eulerAngles = new_pos + transform.eulerAngles;
                break;
            default:
                transform.parent.position = GetMouseWorldPos() + mOffset;
                break;
        }
    }
}
