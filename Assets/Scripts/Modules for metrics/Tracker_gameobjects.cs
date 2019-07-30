using UnityEngine;
using UnityEngine.UI;

/* The purpose of this generic class is to allow the drop and play functionality of tracking every button and slider in the project with one script.
 * This in theory should be efficient and effective however this is still in testing. 
 * 
 * 
 */


public class Tracker_gameobjects : MonoBehaviour {
    public string gameobject_keylogger_obj = "TCP_Server_node_Obj_coordinator";
    private CSV_writer sendee_gameObject; 
    private GameObject obj_being_tracked;
    private Slider slider;  //Type 1
    private Button button;  //Type 2
    private Dropdown drop;  //Type 3
    private Toggle toggle;  //Type 4
    private int type;

    // Use this for initialization
    void Start()
    {
        sendee_gameObject = GetComponent<CSV_writer>();
        try
        {
            slider = this.gameObject.GetComponent<Slider>();
            button = this.gameObject.GetComponent<Button>();
            drop = this.gameObject.GetComponent<Dropdown>();
            toggle = this.gameObject.GetComponent<Toggle>();
        }
        catch
        {
            Debug.Log("Something Happened...");
        }

        //Determine the type that is attached to this game object. 
        if (slider != null)
        {
            type = 1;
        }
        else if (button != null)
        {
            type = 2;
        }
        else if (drop != null)
        {
            type = 3;
        }
        else
        {
            type = 4;
        }


        //Switch implemented in order to determine which UI instrument is being used. 
        switch (type)
        {
            //Sliders
            case 1:
                {
                    slider.onValueChanged.AddListener(slider_callback);
                    break;
                }
            //Buttons
            case 2:
                {
                    button.onClick.AddListener(button_callback);
                    break;
                }
            //Dropdown menus
            case 3:
                {
                    drop.onValueChanged.AddListener(drop_callback);
                    break;
                }
            //Toggles
            case 4:
                {
                    toggle.onValueChanged.AddListener(toggle_callback);
                    break;
                }
        }
    }


    ///Generic callbacks to parent class, this will trace back to the csv writer wherever it may be and send its signal when there was an action. 
    public void slider_callback(float value)
    {
        sendee_gameObject.set_UI_element_inuse(this.name);
    }
    
    public void button_callback()
    {
        sendee_gameObject.set_UI_element_inuse(this.name);
    }
    public void drop_callback(int value)
    {
        sendee_gameObject.set_UI_element_inuse(this.name);
    }
    public void toggle_callback(bool value)
    {
        sendee_gameObject.set_UI_element_inuse(this.name);
    }


}
