using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityDev_Devesh.UI_Screen_Controller
{
    [CustomEditor(typeof(USC_Screens))]
    public class USC_Screens_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            USC_Screens targetScript = (USC_Screens)target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("screen_names"), true);
            EditorGUILayout.Space();
            if (GUILayout.Button("Update"))
                targetScript.CheckAndUpdate_Screens();

            serializedObject.ApplyModifiedProperties();
        }
    }
}