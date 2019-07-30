using UnityEngine;
using UnityEngine.UI;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine.EventSystems;
using System.IO;
using System;

/// <summary>
/// This code attempts to record all data that corresponds to the keystrokes of the tablet by the user. 
/// </summary>

public class Keylogger : MonoBehaviour {
    public InputField output_stream;
    public EventSystem system;
    private float x, y;
    Matrix<double> hor_mat = Matrix<double>.Build.Dense(Screen.width, Screen.height); //Landscape mode
    Matrix<double> ver_mat = Matrix<double>.Build.Dense(Screen.height, Screen.width); //Portrait mode
    public bool enable_logging = true; //Enables or disables the collection of data. 
    string textfile_log_coordinates;
    string mode;
    string name_obj = "No action.";

    void Start()
    {
        textfile_log_coordinates    = "user_actions_file_";
        textfile_log_coordinates    += DateTime.Now.ToString("yyyy_MM_dd_hmmss");
        textfile_log_coordinates    += ".csv";
        textfile_log_coordinates   = fix_path(textfile_log_coordinates);
    }
    


	void LateUpdate () {
        
        //Utilities for picking up mouse input and formatting 
        Vector3 mousy_input = Input.mousePosition;
        x = mousy_input.x;
        y = mousy_input.y;
        try
        {
            //Adquires the name of the last button pressed or slider. 
            //name_obj = system.currentSelectedGameObject.name;
            //Debug.Log(name_obj);
 
        }
        catch
        {
            Debug.Log("Waiting for action.");
        }

        //Creates and stores a heatmap of where the cursor has been using a matrix. 
        var ori = Screen.orientation;
        try
        {
            if (ori == ScreenOrientation.Landscape)
            {
                //Landscape mode
                //hor_mat[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] += 1; //Let the user use the csv file to generate the required data. 
                mode = "landscape";
            }
            else
            {
                //Portrait modes
                //ver_mat[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] += 1; //Let the user use the csv file to generate the required data. 
                mode = "portrait";
            }
        }
        catch
        {
            Debug.Log("Skipping record");
        }
        //Formatting
        string out_msg = "X_touch," + x + ",Y_touch," + y + "," + mode + "," + name_obj;
        output_stream.text = out_msg;

        ////////////////////Block for logging mouse pointer////////////////
        if (enable_logging)
        {
            try
            {
                using (var sw = new StreamWriter(textfile_log_coordinates, true))
                {
                    sw.WriteLine(out_msg);
                    sw.Flush();
                }
            }
            catch
            {
                Console.WriteLine("Could not write to file.");
                Debug.LogError("IO error encountered");
            }
        }

        //Reset the name string after use. 
        name_obj = "";
    }


    private string fix_path(string path_in)
    {
        #if UNITY_EDITOR
                return Application.dataPath + "/Resources/" + path_in;
        #elif UNITY_ANDROID
                        return Application.persistentDataPath + "/" +path_in;
        #elif UNITY_IPHONE
                        return Application.persistentDataPath + "/" +path_in;
        #else
                        return Application.dataPath +"/"+path_in;
        #endif
    }

}
