///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Camera_controls.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_Open_VR V2
//  Revision:        1.0 - 6/22/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Camera control wrapper for object in control. 
//
///////////////////////////////////////////////////////////////////////////////
//Some Code inherited from https://forum.unity.com/threads/moving-main-camera-with-mouse.119525/
//Scrolling and orbit inherited from https://www.youtube.com/watch?v=bVo0YLLO43s 

//Latest version code - https://wiki.unity3d.com/index.php/MouseOrbitImproved

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class Camera_controls : MonoBehaviour
{
    public SingleJoystick trackpad;

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zoomSpeed = 4.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float timeout_for_zoom =   0.5F;

    public Slider zoom;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    private Vector3     starting_pos;
    private Quaternion  starting_rot; 

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        starting_pos = target.transform.position;
        starting_rot = target.transform.rotation;
    }


    //Use this method in combination with a OnClick() button to reset the camera position. 
    public void button_reset_position_and_rotation()
    {
        target.transform.position = starting_pos;
        target.transform.rotation = starting_rot;
    }


    Vector3 mouseOrigin;
    private bool test_me = false;
    void Update()
    {

        //Use this code to determine when the user has clicked down on 
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            test_me = true;
        }

        
        //Old code that was meant for use to move around using the middle mouse functionality with the camera position. 
        if (!Input.GetMouseButtonDown(2)) test_me = false;

        if (test_me)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
        
    }

    bool change_in_slider; 

    //Event-specific hack to detect when the user has moved the zoom bar
    public void is_being_dragged()
    {
        change_in_slider = true; 
    }

    //When the user decides to double tap the screen, the movement across the screen is tracked and the camera is moved accordingly. Additional controls are in place to 
    // allow the zoom function to operate smoothly as well (Note the timeout parameter) 
    void LateUpdate()
    {
        Vector3 dir = trackpad.GetInputDirection() * 0.5F;
        if (target && Input.GetAxis("Fire2") != 0 || change_in_slider == true || dir.x > 0)
        {
            change_in_slider = false; 
            x += Input.touches[0].deltaPosition.x * xSpeed * distance * 0.02f;
            y -= Input.touches[0].deltaPosition.y * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            distance = zoom.value;

            //Vectors that allow the transformation of the camera in the world space
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

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
            target.transform.Translate(desiredMoveDirection * Time.deltaTime);
           

        }
      
    }

    //Limits in place for the maximum angle the camera should look. 
    public static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

/*
using UnityEngine;
using UnityEngine.UI;
public class Camera_controls : MonoBehaviour
{
    //Limits for camera 
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;
    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;
    private Vector3 shift;

    public Transform target_obj;
    private Vector2 input;  

    //Speed Limits
    public float horizontal_speed = 1;
    public float vertical_speed = 1;
    public float foward_speed = 1;
    public float turn_speed = 1; 
 
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;
    private float old_zoom; 
    public Slider zoom_amt;
    public Toggle move_cam; 


    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
        zoom_amt.value = _CameraDistance;
        old_zoom = _CameraDistance;
        zoom_amt.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    //Check for a change in the direction of movment of the mouse against the screen 
    void ValueChangeCheck()
    {
        _CameraDistance = zoom_amt.value; 
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if ()
        //    CameraDisabled = !CameraDisabled;

        //if (Input.GetMouseButton(1))
        //old_zoom = _CameraDistance;
        float slide_bar = zoom_amt.value;
        if (Input.GetAxis("Fire2") != 0 )
        {
            
            //input += new Vector2(Input.GetAxis("Mouse X") * turn_speed, Input.GetAxis("Mouse Y") * turn_speed);
           // transform.localRotation = Quaternion.Euler(input.y, input.x, 0);
            //transform.localPosition = target_obj.position - (transform.localRotation * Vector3.forward * _CameraDistance);
            
          
            _LocalRotation.x += Input.GetAxis("Mouse X") * turn_speed;
            _LocalRotation.y += Input.GetAxis("Mouse Y") * turn_speed;

            //Clamp the y Rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < 0f)
                _LocalRotation.y = 0f;
            else if (_LocalRotation.y > 90f)
                _LocalRotation.y = 90f;

            //if (Input.GetButton("LShift"))
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
                ScrollAmount *= (this._CameraDistance * 0.3f);
                this._CameraDistance += ScrollAmount * -1f;
                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 0.5f, 100f);
            }

            //Actual Camera Rig Transformations
            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

            if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
            {
                this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }
            
        }

        //Conditionals adjusted for camera viewset
        if (Input.GetButton("Foward"))
        {
            shift = new Vector3(0, foward_speed, 0);
            _XForm_Parent.transform.Translate(shift);
        }

        if (Input.GetButton("Back"))
        {
            shift = new Vector3(0, -foward_speed, 0);
            _XForm_Parent.transform.Translate(shift);
        }
        if (Input.GetButton("Left"))
        {
            shift = new Vector3(-horizontal_speed, 0, 0);
            _XForm_Parent.transform.Translate(shift);
        }

        if (Input.GetButton("Right"))
        {
            shift = new Vector3(horizontal_speed, 0, 0);
            _XForm_Parent.transform.Translate(shift);
        }

    }
}


    */
//float hor = horizontal_speed * Input.GetAxis("Mouse Y");
//float ver = vertical_speed * Input.GetAxis("Mouse X");


//Clamp the y Rotation to horizon and not flipping over at the top
//if (this.transform.localRotation.eulerAngles.y < 0f)
//    hor = 0f;
//else if (this.transform.localRotation.eulerAngles.y > 90f)
//    hor = 0;

//transform.Rotate(hor, ver, 0);
