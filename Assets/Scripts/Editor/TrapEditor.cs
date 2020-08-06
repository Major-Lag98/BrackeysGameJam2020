using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArrowTrap))]
public class TrapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ArrowTrap myTarget = (ArrowTrap)target;

        DrawDefaultInspector();

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

        if (GUILayout.Button("Randomize Initial Delay"))
        {
            myTarget.InitialDelay = Random.Range(0, myTarget.FireDelay);
        }
    }
}