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
    string textfile_log_buttonpresses;
    string mode; 

    void Start()
    {
        textfile_log_coordinates    = "Coord_text_";
        textfile_log_buttonpresses  = "Button_presses_";

        textfile_log_coordinates    += DateTime.Now.ToString("hmmss");
        textfile_log_buttonpresses  += DateTime.Now.ToString("hmmss");

        textfile_log_coordinates    += ".txt";
        textfile_log_buttonpresses  += ".txt";
    }
    /*
	void LateUpdate () {
        //Utilities for picking up mouse input and formatting 
        Vector3 mousy_input = Input.mousePosition;
        x = mousy_input.x;
        y = mousy_input.y; 
        

        //Adquires the name of the last button pressed or slider. 
        string name_obj = system.currentSelectedGameObject.name;

        //Creates and stores a heatmap of where the cursor has been using a matrix. 
        var ori = Screen.orientation;
        if (ori == ScreenOrientation.Landscape)
        {
            //Landscape mode
            hor_mat[(int)x,(int)y] += 1;
            mode = "landscape";
        }
        else
        {
            //Portrait mode
            ver_mat[(int)x, (int)y] += 1;
            mode = "portrait"; 
        }

        //Formatting
        string out_msg = "X: " + x + " Y: " + y + " " + mode;
        output_stream.text = out_msg;

        ////////////////////Block for logging mouse pointer////////////////
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


        ///////////////////Block for logging buttons///////////////////////
        try
        {
            using (var sw = new StreamWriter(textfile_log_buttonpresses, true))
            {
                sw.WriteLine(name_obj);
                sw.Flush();
            }
        }
        catch
        {
            Console.WriteLine("Could not write to file.");
            Debug.LogError("IO error encountered");
        }


    }*/
}
