using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Text;

//Starts using code from https://shanemartin2797blog.wordpress.com/2015/11/20/how-to-read-from-and-write-to-csv-in-unity/
public class CSV_writer : MonoBehaviour
{
    public string file_name;        //The file name being selected as a starting file to work with. 
    public Dropdown Dropdown_menu;  //UI element
    private string path = "C:/elwood.campus.nist.gov/735/users/ems9/My Documents/Manus VR Unity Folder - 2019/Android App - Intelligent Sys Div/Assets/CSV Files";
    //The pathname where the CSV files are going to be stored
    List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    // The literal data inside a CSV file
    List<string> string_list = new List<string>(); //List of CSV file names in string format 
    UR5_to_TPC converter;     //Workaround to get data that's being sent to the robot. 

    public float update_rate = 2.0F; //The update rate to save to a csv file
    public bool update_save = false;  //Check to start saving data to a CSV file or not
    private string temp_name;

    // Use this for initialization
    void Start()
    {
        converter = GetComponent<UR5_to_TPC>();
        update_list();
        float module = System.DateTime.Today.Millisecond;
        file_name = path;
        file_name += "/CSV_Rbt_dat_";
        file_name += module.ToString();
        file_name += ".csv";

        temp_name = file_name;
        old_time = Time.time;

 
    }

    //Function to update the dropdown menu with all possible files in the CSV folder
    void update_list()
    {
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (FileInfo f in fileInfo)
        {
            string_list.Add(f.Name);
        }
        Dropdown_menu.ClearOptions();
        Dropdown_menu.AddOptions(string_list);
    }

    //Runs two functionalities, checking what the dropbox menu has and also rate of data storage.
    float old_time; 
    void Update()
    {
        file_name = string_list[Dropdown_menu.value];

        float new_time = Time.time;
        if (update_save && new_time  > old_time + update_rate )
        {
            store_data(); 
        }
    }
    
    //Load in data from a csv and store for later use.
    public void load_data(string file_name)
    {
        data = CSVReader.Read(file_name);
        counter = 0;
    }

    //Phrase one line of data, returns one output_string block
    int counter; 
    public string recall_line()
    {
        string line = (string)data[counter]["Angle-Joints"] + (string)data[counter]["RbtID"] +
            (string)data[counter]["GripperStat"] + (string)data[counter]["DO1"] + (string)data[counter]["DO2"]
            + (string)data[counter]["DO3"] + (string)data[counter]["DO4"];
        counter++;
        return line;
    }
    
    //Stores the header and appends data to a file.
    public void store_data()
    {
        using (var sw = new StreamWriter(temp_name))
        {
            
            //in your loop

            if (!File.Exists(temp_name))
            {
                var newLine = "Angle-Joints,RbtID,GripperStat,DO1,DO2,DO3,DO4";
                sw.WriteLine(newLine);
            }else
            {
                sw.WriteLine(Time.time.ToString());
            }
            sw.Flush();

             //after your loop
             /*
            if (!File.Exists(file_name))
            {
                string header = "Angle-Joints,RbtID,GripperStat,DO1,DO2,DO3,DO4";
                sw.WriteLine(header);
            }*/
            //string filePath = getPath();

            // sw.WriteLine(Time.time);
            //File.AppendAllText(file_name, converter.get_message());
        }


        update_list();
    }


    //Redundant 
    private string getPath()
    {
        #if UNITY_EDITOR
                return Application.dataPath + "/CSV/" + "Saved_Inventory.csv";
        #elif UNITY_ANDROID
                        return Application.persistentDataPath+"Saved_Inventory.csv";
        #elif UNITY_IPHONE
                        return Application.persistentDataPath+"/"+"Saved_Inventory.csv";
        #else
                return Application.dataPath + "/" + "Saved_Inventory.csv";
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
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

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
                value.Replace("Robot Utilities:", "");
                value.Replace(";", "");
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
    }
}
 