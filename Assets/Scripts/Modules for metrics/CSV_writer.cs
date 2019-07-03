using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Text;


/// <summary>
/// This is a basic CSV file writer meant for writing out to files for later review. The process works by 
/// starting out with a user-given name for which the user can use to apply to a file to store data through.
/// 
/// The user can also load up a file from a dropdown menu from previous recordings. 
/// 
/// 
/// 
/// 
/// </summary>


//Starts using code from https://shanemartin2797blog.wordpress.com/2015/11/20/how-to-read-from-and-write-to-csv-in-unity/
public class CSV_writer : MonoBehaviour
{
    public string file_name;        //The file name being selected as a starting file to work with. 
    public Dropdown Dropdown_menu;  //UI element
    //All CSV files must go into the resources folder in order for Unity to find them
    //The pathname where the CSV files are going to be stored
    List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    // The literal data inside a CSV file
    List<string> string_list = new List<string>(); //List of CSV file names in string format 
    UR5_to_TPC converter;     //Workaround to get data that's being sent to the robot. 


    public string output_csv; 
    private string temp_name;
    public GameObject obj_box;
    public GameObject indicator;
    public Toggle toggle_recording;
    public Slider refresh_rate;
    public Toggle toggle_replay; 
    

    //Get time from https://stackoverflow.com/questions/296920/how-do-you-get-the-current-time-of-day
    // Use this for initialization
    void Start()
    {
        converter = GetComponent<UR5_to_TPC>();
        update_list();
        //file_name = path;
        file_name += "/CSV_dat_";
        file_name += DateTime.Now.ToString("hmmss");
        file_name += ".csv";
        file_name = fix_path(file_name);
        temp_name = file_name;
        old_time = Time.time;

        //Write the header to the presently opened file and include its unique name. 
        try
        {
            using (var sw = new StreamWriter(temp_name,true))
            {
                var newLine = "Joint_1,Joint_2,Joint_3,Joint_4,Joint_5,Joint_6,RbtID,GripperStat,DO1,DO2,DO3,DO4,Bypass,Chng_Rbots,X,Y,Z,Q_X,Q_Y,Q_Z,Q_W"; 
                sw.WriteLine(newLine);
                sw.Flush();
            }
            update_list();
        }
        catch
        {
            Console.WriteLine("Could not write to file.");
            Debug.LogError("IO error encountered");
        }
    }

    //https://answers.unity.com/questions/16433/get-list-of-all-files-in-a-directory.html
    //Function to update the dropdown menu with all possible files in the CSV folder
    void update_list()
    {
        var info = new DirectoryInfo(fix_path(""));
        //Calls in and dumps all the string names into an array
        var fileInfo = info.GetFiles();
        string_list.Clear(); 
        foreach (FileInfo f in fileInfo)
        {
            string_list.Add(f.Name);
        }
        //Clear the menu and add in all the given values. 
        Dropdown_menu.ClearOptions();
        Dropdown_menu.AddOptions(string_list);
    }

    //Runs two functionalities, checking what the dropbox menu has and also rate of data storage.
    float old_time; //Time keeeping for timers
    void Update()
    {
        //This selects the string from which the dropdown menu was selected from. 
        file_name = string_list[Dropdown_menu.value];
        float new_time = Time.time;

        //Refresh the data at a slider-specific rate and also when toggled to do so. 
        if (toggle_recording.isOn && !toggle_replay.isOn && new_time  > old_time + refresh_rate.value)
        { 
            //Calls the CSV file to append dataw
            store_data();
            old_time = Time.time;
        }

        //Recall a line with a given refresh rate and when toggled to do so. 
        if (!toggle_recording.isOn && toggle_replay.isOn && new_time > old_time + refresh_rate.value)
        {
            recall_line();
            old_time = Time.time;
        }
    }

    public void change_csv_save()
    {
        temp_name = file_name;
    }
    
    //Load in data from a csv and store for later use.
    public void load_data()
    {
        //string temp_path = "Android App - Intelligent Sys Div/Assets/CSV Files/";
        data = CSVReader.Read(fix_path(file_name));
        //Console.WriteLine(data);

        update_list();
        counter = 0;
        max_limit = data.Count;
        if(max_limit == 0)
        {
            Console.WriteLine("Warning: No data!");
        }

    }

    //Phrase one line of data, returns one output_string block
    int counter;
    int max_limit; 
    public void recall_line()
    {
        if (counter < max_limit)
        {
            indicator.transform.position = new Vector3(str2flt("X"), str2flt("Y"), str2flt("Z"));
            indicator.transform.rotation = new Quaternion(str2flt("Q_X"), str2flt("Q_Y"), str2flt("Q_Z"), str2flt("Q_W"));
        }
        else
        {
            counter = 0;
            indicator.transform.position = new Vector3(str2flt("X"), str2flt("Y"), str2flt("Z"));
            indicator.transform.rotation = new Quaternion(str2flt("Q_X"), str2flt("Q_Y"), str2flt("Q_Z"), str2flt("Q_W"));
        }
        counter++;
     }


    //Autoconvert a string value from the data field into a float value and return
    float str2flt(string value)
    {
        return (float)Convert.ToDouble(data[counter][value]);
    }

    //Stores the header and appends data to a file.
    //Best reference: https://www.youtube.com/watch?v=vDpww7HsdnM
    public void store_data()
    {
        try
        {
            using (var sw = new StreamWriter(temp_name,true))
            {
                //Get the pose message being sent to robot. 
                string temp_msg = converter.get_message();
                temp_msg = temp_msg.Replace(";", "");
                temp_msg = temp_msg.Replace("UR5_pos:", "");
                temp_msg = temp_msg.Replace("Robot Utilities:",",");
                temp_msg = temp_msg.Replace("\n", "");

                //Write in the position and quaternion of the control node in Unity

                var box_msg = obj_box.transform.position.ToString();
                box_msg += obj_box.transform.rotation.ToString();
                box_msg = box_msg.Replace("(", "");
                box_msg = box_msg.Replace(")", ",");
                temp_msg = temp_msg +"," + box_msg; 
                
                //Write it out to a CSV file
                sw.WriteLine(temp_msg);
                sw.Flush();
            }
        }
        catch
        {
            Console.WriteLine("Could not write to file.");
            Debug.LogError("IO error encountered");
        }
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


//CSV Reader class from https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        
#if UNITY_EDITOR
        file = file.Replace(".csv", "");
        Debug.Log(file);
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;
        //TextAsset data = AssetDatabase.LoadAssetAtPath(file, typeof(TextAsset);

        //Debug.Log(data.ToString());
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

        
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
#elif UNITY_ANDROID
        var list = new List<Dictionary<string, object>>();
        string buffer_line = "";
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    buffer_line += line + "\n";

                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        //Console.WriteLine("Final Msg: " + buffer_line);

        //Debug.Log(data.ToString());
        var lines = Regex.Split(buffer_line, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);


        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
        
#else
        return var list = new List<Dictionary<string, object>>();    
#endif
    }
}






//Debug.Log(data[counter]["X"]);
//Debug.Log(data[counter]["Y"]);
//Debug.Log(data[counter]["Z"]);
/*string line = (string)data[counter]["Angle-Joints"] + (string)data[counter]["RbtID"] +
    (string)data[counter]["GripperStat"] + (string)data[counter]["DO1"] + (string)data[counter]["DO2"]
    + (string)data[counter]["DO3"] + (string)data[counter]["DO4"];
*/
