using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DimensionalMapGen)),  CanEditMultipleObjects]
public class TerraGenEditor : Editor
{
    public override void OnInspectorGUI() {
        DimensionalMapGen terraGen = (DimensionalMapGen)target;

        if (DrawDefaultInspector()) 
        {
            if (terraGen.autoUpdate) 
            {
                terraGen.GenDMap();
            }
        }
        if (GUILayout.Button("Generate")) 
        {
            terraGen.GenDMap();
        }
    }
}
