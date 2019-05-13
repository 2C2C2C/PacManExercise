using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TestFuncs))]
[CanEditMultipleObjects]
public class TestFuncsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestFuncs myScript = (TestFuncs)target;
        // btn for init soldier 
        if (GUILayout.Button("change soldier weapon"))
        {
            myScript.TestFunc1();
        }
        EditorGUILayout.Space();

        // btn for init soldier 
        if (GUILayout.Button("restart"))
        {
            LevelGenerator.ReStartTest();
        }
        EditorGUILayout.Space();

        // 
        wa1 = EditorGUILayout.TextField(wa1);
        if (GUILayout.Button("TestSave level 01"))
        {
            LevelData ld = new LevelData
            {
                name = "test",
                grids = new int[,]
            {
                { 0,0},
                { 1,1},
            }
            };

            HentaiTools.SLSomeData.Instance.SaveData<LevelData>(wa1, ld);
        }
        EditorGUILayout.Space();

        // 

        wa2 = EditorGUILayout.TextField(wa2);
        if (GUILayout.Button("TestLoad level 01"))
        {
            LevelData ld;
            ld = HentaiTools.SLSomeData.Instance.GetData<LevelData>(wa2);
            if (wa2 != null)
                Debug.Log($"{ld.name} {ld.grids} ");

        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Label("your name");
        recorderName1 = EditorGUILayout.TextField(recorderName1);
        if (GUILayout.Button("save record test"))
        {
            string fName = recorderName1 + System.DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            HentaiTools.RecordData rd = new HentaiTools.RecordData(recorderName1, UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), System.DateTime.UtcNow.ToString("F", new System.Globalization.CultureInfo("fr-FR")));
            HentaiTools.SLSomeData.Instance.SaveData<HentaiTools.RecordData>(fName, rd);

        }
        EditorGUILayout.Space();

        GUILayout.Label("file name");
        recorderName2 = EditorGUILayout.TextField(recorderName2);
        if (GUILayout.Button("load record test"))
        {
            HentaiTools.RecordData rd = HentaiTools.SLSomeData.Instance.GetData<HentaiTools.RecordData>(recorderName2);
            Debug.Log($"{rd.name}");
            Debug.Log($"{rd.score}");
            Debug.Log($"{rd.whenItRecord}");
        }
        EditorGUILayout.Space();


    }
    string wa1, wa2;
    string recorderName1, recorderName2;



    // class end
}
