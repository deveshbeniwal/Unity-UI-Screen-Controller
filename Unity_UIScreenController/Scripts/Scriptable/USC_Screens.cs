using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UnityDev_Devesh.UI_Screen_Controller
{
    public class USC_Screens : ScriptableObject
    {
        public List<string> screen_names;

        public string Screen_FolderPath
        {
            get { return string.Format("{0}/{1}/{2}", Application.dataPath, "Scripts", "Screens"); }
        }
        public string Asset_FolderPath
        {
            get { return string.Format("{0}/{1}/{2}/{3}", Application.dataPath, "UIScreenController", "Scripts", "Enums"); }
        }

        public async void CheckAndUpdate_Screens()
        {
            for (int i = 0; i < screen_names.Count; i++)
                screen_names[i] = screen_names[i].Replace(' ', '_');

            foreach (var item in screen_names)
            {
                string filename = "Screen_" + item;
                string path = string.Format("{0}/{1}.cs", Screen_FolderPath, filename);

                if (File.Exists(path))
                    return;

                await Create_Class(filename, path);
                await Update_Enum();

                AssetDatabase.Refresh();
            }
        }


        private async Task Create_Class(string filename, string filepath)
        {
            if (!Directory.Exists(Screen_FolderPath))
                Directory.CreateDirectory(Screen_FolderPath);

            StreamWriter outfile = new StreamWriter(filepath);
            await outfile.WriteLineAsync("using LS_DEV.UI_Screen_Controller;");
            await outfile.WriteLineAsync("");
            await outfile.WriteLineAsync("public class " + filename + " : USC_BaseScreen");
            await outfile.WriteLineAsync("{");
            await outfile.WriteLineAsync("\tpublic override void Initialize()");
            await outfile.WriteLineAsync("\t{");
            await outfile.WriteLineAsync("\t\tbase.screen_type = USC_SCREENS."+ filename.Replace("Screen_", "") + ";");
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
            StreamWriter outfile = new StreamWriter(Asset_FolderPath + "/USC_Enums.cs");
            await outfile.WriteLineAsync("namespace LS_DEV.UI_Screen_Controller");
            await outfile.WriteLineAsync("{");
            await outfile.WriteLineAsync("\tpublic enum USC_SCREENS");
            await outfile.WriteLineAsync("\t{");

            foreach (var item in screen_names)
                await outfile.WriteLineAsync("\t\t" + item);

            await outfile.WriteLineAsync("\t}");
            await outfile.WriteLineAsync("}");
            outfile.Close();
        }
    }
}