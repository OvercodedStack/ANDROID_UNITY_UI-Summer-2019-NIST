using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using RedScarf.EasyCSV;

namespace RedScarf.EasyCsvEditor
{
    /// <summary>
    /// CSV格式转换窗口
    /// </summary>
    public class CsvFormatConvertWindow : EditorWindow
    {
        static string selectFile;
        static string selectDirectiry;
        static CsvTable.LineBreak saveLienBreak= CsvTable.LineBreak.CRLF;
        static char separator=CsvTable.DEFAULT_SEPARATOR;

        [MenuItem("Tools/EasyCsv/Csv Format Convert Window")]
        static void Init()
        {
            var window = EditorWindow.GetWindow<CsvFormatConvertWindow>();
            window.Show();
            window.titleContent = new GUIContent("Csv Format Convert Window");

            if (string.IsNullOrEmpty(selectDirectiry))
            {
                selectDirectiry = Application.dataPath;
            }
        }

        private void OnGUI()
        {
            var enable = GUI.enabled;

            using (var scope =new GUILayout.HorizontalScope())
            {
                if (GUILayout.RepeatButton(new GUIContent("Select"),GUILayout.Width(60)))
                {
                    selectFile = EditorUtility.OpenFilePanelWithFilters("Select csv", selectDirectiry, new string[] { "Csv File","csv"});
                    if(!string.IsNullOrEmpty(selectFile))
                        selectDirectiry = Path.GetDirectoryName(selectFile);
                }
                GUI.enabled = false;
                EditorGUILayout.TextArea(selectFile);
            }

            GUILayout.FlexibleSpace();

            GUI.enabled = !string.IsNullOrEmpty(selectFile);

            using (var scope = new GUILayout.HorizontalScope())
            {
                saveLienBreak = (CsvTable.LineBreak)EditorGUILayout.EnumPopup("Line break", saveLienBreak);

                EditorGUILayout.Space();

                var newSeparator = EditorGUILayout.TextField("Separator", separator.ToString());
                char c;
                if(System.Char.TryParse(newSeparator,out c))
                {
                    separator = c;
                }
            }
            if (GUILayout.RepeatButton(new GUIContent("Save as...")))
            {
                var saveFile = EditorUtility.SaveFilePanel("Save csv", selectDirectiry, "Csv File", "csv");
                if (!string.IsNullOrEmpty(saveFile))
                {
                    var csvTable = new CsvTable("", File.ReadAllText(selectFile),false,false);
                    var csvData= csvTable.GetData(saveLienBreak, separator);
                    File.WriteAllText(saveFile, csvData);

                    AssetDatabase.Refresh();

                    EditorUtility.DisplayDialog("Successful",saveFile, "Ok", "Cancel");
                }
            }

            GUI.enabled = enable;
        }
    }
}