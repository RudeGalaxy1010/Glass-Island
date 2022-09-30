using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Reset progress"))
        {
            ((LevelManager)target).ResetProgress();
            Debug.Log("Progress reseted");
        }
    }
}
