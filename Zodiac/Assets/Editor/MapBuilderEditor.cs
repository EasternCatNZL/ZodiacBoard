using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapBuilder))]
public class MapBuilderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //get script
        MapBuilder builderScript = (MapBuilder)target;
        //set button logic
        if(GUILayout.Button("Build Map"))
        {
            builderScript.BuildMap();
        }
    }
}
