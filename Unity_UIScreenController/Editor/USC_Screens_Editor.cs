using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UnityDev_Devesh.UI_Screen_Controller
{
    public class USC_Screens_Editor : EditorWindow
    {
        public string[] all_screens;
        SerializedObject so;

        public string Screen_FolderPath
        {
            get { return string.Format("{0}/{1}/{2}", Application.dataPath, "Scripts", "Screens"); }
        }



        [MenuItem("Window/Unity UI Screen Controller")]
        static void OpenWindow()
        {
            GetWindow(typeof(USC_Screens_Editor));
        }


        private void Awake()
        {
            all_screens = new string[] { };

            string jsonFilePath = String.Format("{0}/Resources/USC_Data.txt", Application.dataPath);
            if (!File.Exists(jsonFilePath))
                return;

            var jsonFileText = File.ReadAllText(jsonFilePath);
            all_screens = JsonUtility.FromJson<USC_JSON_FORMATTER>(jsonFileText).data;
        }
        private void OnEnable()
        {
            ScriptableObject target = this;
            so = new SerializedObject(target);
        }

        void OnGUI()
        {
            so.Update();

            SerializedProperty stringsProperty = so.FindProperty("all_screens");
            EditorGUILayout.PropertyField(stringsProperty, true);

            so.ApplyModifiedProperties();

            if (GUILayout.Button("Update"))
                CheckAndUpdateScreens();

            if (GUILayout.Button("Add/Update UI Screen Manager"))
                AddOrUpdate_ScreenManager();
        }


        private async void CheckAndUpdateScreens()
        {
            for (int i = 0; i < all_screens.Length; i++)
                all_screens[i] = all_screens[i].Replace(' ', '_');

            foreach (var item in all_screens)
            {
                string filename = "Screen_" + item;
                string path = string.Format("{0}/{1}.cs", Screen_FolderPath, filename);

                if (!File.Exists(path))
                {
                    await Save_Json();
                    await Create_Class(filename, path);
                    await Update_Enum();

                    AssetDatabase.Refresh();
                }
            }
        }
        private void AddOrUpdate_ScreenManager()
        {
            USC_ScreenManager manager = GameObject.FindObjectOfType<USC_ScreenManager>();
            if(manager == null) 
            {
                GameObject controller_obj = new GameObject("--[ UI_Screen_Manager ]--");
                manager = controller_obj.AddComponent<USC_ScreenManager>();
            }

            manager.RefreshAssets();
        }

        private async Task Save_Json()
        {
            USC_JSON_FORMATTER serializableData = new USC_JSON_FORMATTER
            { 
                data = all_screens
            };

            string resourcesPath = Application.dataPath + "/Resources";
            if (!Directory.Exists(resourcesPath))
                Directory.CreateDirectory(resourcesPath);

            string jsonFilePath = resourcesPath + "/USC_Data.txt";
            StreamWriter outStream = new StreamWriter(jsonFilePath);
            await outStream.WriteLineAsync(JsonUtility.ToJson(serializableData));
            outStream.Close();
        }

        private async Task Create_Class(string filename, string filepath)
        {
            if (!Directory.Exists(Screen_FolderPath))
                Directory.CreateDirectory(Screen_FolderPath);

            StreamWriter outfile = new StreamWriter(filepath);
            await outfile.WriteLineAsync("using UnityDev_Devesh.UI_Screen_Controller;");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("public class " + filename + " : USC_BaseScreen");
            await outfile.WriteLineAsync("{");
            await outfile.WriteLineAsync("\tpublic override void Initialize()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.screen_type = USC_SCREENS." + filename.Replace("Screen_", "") + ";");
            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("\tpublic override void Show()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.Show();");
            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("\tprotected override void ShowCompleted()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.ShowCompleted();");
            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("\tpublic override void Hide()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.Hide();");
            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("\tprotected override void HideCompleted()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.HideCompleted();");
            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("}");
            outfile.Close();
        }

        private async Task Update_Enum()
        {
            string filepath = System.Array.Find(AssetDatabase.GetAllAssetPaths(), x => x.Contains("USC_Enums"));

            StreamWriter outfile = new StreamWriter(filepath);
            await outfile.WriteLineAsync("namespace UnityDev_Devesh.UI_Screen_Controller");
            await outfile.WriteLineAsync("{");
            await outfile.WriteLineAsync("\tpublic enum USC_SCREENS");
            await outfile.WriteLineAsync("\t{");

            foreach (var item in all_screens)
                await outfile.WriteLineAsync("\t\t" + item + ",");

            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("}");

            outfile.Close();
        }
    }

    [System.Serializable]
    public class USC_JSON_FORMATTER
    {
        public string[] data;
    }
}
