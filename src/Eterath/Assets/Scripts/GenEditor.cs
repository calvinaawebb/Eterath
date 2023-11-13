using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGen)),  CanEditMultipleObjects]
public class GenEditor : Editor
{
    public override void OnInspectorGUI() {
        MapGen mapGen = (MapGen)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) 
        {
            mapGen.GenerateMap();
        }
    }
}
