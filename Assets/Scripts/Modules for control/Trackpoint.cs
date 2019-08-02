///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Trackpoint.cs
//  Subsystem:       Human-Robot Interaction with alternative UI controls
//  Workfile:        Android App 
//  Revision:        1.0 - 7/11/2019
//  Author:          Esteban Segarra
//
//  Description
//  ===========
// This script simply transforms the point that the camera is looking at using a joystick control prefab. 
// This can also be used to move other objects in the world space. 
//
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class Trackpoint : MonoBehaviour {
    public SingleJoystick trackpad;
    // Update is called once per frame
 
    void LateUpdate()
    {
        Vector3 dir = trackpad.GetInputDirection() * 0.5F;
        //reading the input:
        //float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        //float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical");

        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * dir.x + right * dir.y;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * Time.deltaTime);
     }
}
