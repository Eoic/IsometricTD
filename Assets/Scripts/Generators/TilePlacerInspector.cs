using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilePlacer))]
public class TilePlacerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TilePlacer generator = (TilePlacer)target;

        if (GUILayout.Button("Place"))
            generator.Place();
    }
}
