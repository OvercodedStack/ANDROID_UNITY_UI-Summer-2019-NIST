using UnityEngine;
using System.Collections;
using System;
using System.IO;

namespace RedScarf.EasyCSV.Demo
{
    public class CsvTest : MonoBehaviour
    {
        public TextAsset text;
        CsvTable table;

        private void Start()
        {
            CsvHelper.Init();
            table = CsvHelper.Create(text.name, text.text);
        }

        int row=0;
        int column=0;
        string rowStr = "";
        string columnStr="";
        string readValue="";
        string writeValue="";
        string rowDataStr="";
        string rowID = "Jack";
        int buttonWidth = 150;

        private void OnGUI()
        {
            GUILayout.Space(30);

            //Display csv table
            foreach (var row in table.RawDataList)
            {
                using (new GUILayout.HorizontalScope())
                {
                    foreach (var value in row)
                    {
                        GUILayout.Label(value,GUILayout.Width(150));
                    }
                }
            }

            GUILayout.Space(100);

            //Modify UI
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Row:",GUILayout.Width(buttonWidth));
                rowStr = GUILayout.TextField(rowStr);
                int.TryParse(rowStr, out row);
                rowStr = row.ToString();

                GUILayout.Space(20);

                GUILayout.Label("Column:", GUILayout.Width(buttonWidth));
                columnStr = GUILayout.TextField(columnStr);
                int.TryParse(columnStr, out column);
                columnStr = column.ToString();
            }
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Read", GUILayout.Width(buttonWidth)))
                {
                    readValue = table.Read(row,column);
                }
                GUILayout.TextArea(readValue);
            }
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Write",GUILayout.Width(buttonWidth)))
                {
                    table.Write(row,column,writeValue);
                }
                writeValue = GUILayout.TextArea(writeValue);
            }
            using (new GUILayout.HorizontalScope())
            {
                using (new GUILayout.VerticalScope(GUILayout.Width(buttonWidth)))
                {
                    if (GUILayout.Button("PaddingData"))
                    {
                        var testRowData = CsvHelper.PaddingData<TestRowData>(text.name, rowID);
                        rowDataStr = testRowData.ToString();
                    }
                    rowID = GUILayout.TextField(rowID);
                }
                GUILayout.TextArea(rowDataStr);
            }
        }
    }
}