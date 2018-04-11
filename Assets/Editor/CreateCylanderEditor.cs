using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateCylander))]
public class CreateCylanderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CreateCylander _script = (CreateCylander)target;
        if (GUILayout.Button("Build Cylander"))
        {
            _script.BuildCylander();
        }
    }
}