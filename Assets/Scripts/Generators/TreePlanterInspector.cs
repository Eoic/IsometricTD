using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreePlanter))]
public class TreePlanterInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TreePlanter planter = (TreePlanter)target;

        if (GUILayout.Button("Generate"))
            planter.PlantTrees();
    }
}
