using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Newtonsoft.Json;

namespace HentaiTools
{
    public class JsonLevelExportWindow : EditorWindow
    {

        private string levelName;
        private int[,] grids =
        {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };

        [MenuItem("Tools/JSON Level Exporter")]
        public static void ShowWindow()
        {
            var window = GetWindow<JsonLevelExportWindow>();
            window.Show();
        }

        // sort of like update
        private void OnGUI()
        {
            GUILayout.Label("Json Level Export Window");
            EditorGUILayout.Space();
            GUILayout.Label("Plaz name your level to export");
            // draw a text field, and assign the value dat user type in
            levelName = EditorGUILayout.TextField(levelName);

            if (!GUILayout.Button("wa")) return;

            var level = new LevelData
            {
                name = levelName,
                grids = grids
            };

            // we can convert any c# obj, to a JSON string
            string JsonData = JsonConvert.SerializeObject(level);

            // create a text file, and save it to our asssssssset/Levels Folder
            //    System.IO.StreamWriter file = new System.IO.StreamWriter((@"./Asset/LevelData/" + levelName), true)
            //    {
            //        file.Write(JsonData);
            //};

            //System.IO.File.WriteAllText((@"./Assets/LevelData/" + levelName), JsonData);
            System.IO.File.WriteAllText($"{Application.dataPath}/MacMan/AppData/LevelData/{levelName}.json", JsonData);

        }
        // class end
    }

    // namespace end
}
