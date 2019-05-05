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

    }

// class end
}
