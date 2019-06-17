using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

//Starts using code from https://shanemartin2797blog.wordpress.com/2015/11/20/how-to-read-from-and-write-to-csv-in-unity/


public class CSV_writer : MonoBehaviour
{
    public string file_name;
    public Dropdown Dropdown_menu;
    public string selected_file;
    private string path = "C:/elwood.campus.nist.gov/735/users/ems9/My Documents/Manus VR Unity Folder - 2019/Android App - Intelligent Sys Div/Assets";
    private FileInfo[] data;

    List<string> string_list = new List<string>();

    // Use this for initialization
    void Start()
    {

        update_list();
        float module = Time.time;
        file_name = "CSV_Rbt_dat_";
        file_name += module.ToString();
        file_name += ".csv";
    }

    // Update is called once per frame
    void Update()
    {

    }

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

    void recall_data()
    {




        //These are the variables I set
        /*
        string type = "";
        string name = "";
        float damage = 0;
        float attackSpeed = 0;
        float critChance = 0;
        float critMultiplier = 1;
        */
        //This is to get all the lines
        //string[] lines = file.text.Split("\n"[0]);

        /*
        for (var i = 0; i < lines.Length; i++)
        {
            //This is to get every thing that is comma separated
            string[] parts = lines[i].Split(","[0]);

            type = parts[0];
            name = parts[1];
            /*
            float.TryParse(parts[2], out damage);
            float.TryParse(parts[3], out attackSpeed);
            float.TryParse(parts[4], out critChance);
            float.TryParse(parts[5], out critMultiplier);


            */
        //THis just makes a cube because why not, should be what the object is
        //GameObject gameObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //This changes the name of the cube to the read name
        //gameObj.name = name;


        // }

    }

    void save_data()
    {
        string filePath = getPath();

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);

        //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("Type,Name,Damage/Armor,AttackSpeed,CritChance,CritDamage");

        /*
        //This loops through everything in the inventory and sets the file to these.
        for (int i = 0; i < inventory.Count; ++i)
        {
            writer.WriteLine(inventory[i].GetType().ToString() +
                "," + inventory[i].name +
                "," + inventory[i].damage.ToString("0.00") +
                "," + inventory[i].attackSpeed.ToString("0.00") +
                "," + inventory[i].critChance.ToString("0.00") +
                "," + inventory[i].critMultiplier.ToString("0.00"));
        }

    */
        writer.Flush();
        //This closes the file
        writer.Close();

        update_list();
    }

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

