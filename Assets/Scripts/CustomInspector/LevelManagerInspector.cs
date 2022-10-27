using UnityEditor;
using UnityEngine;

namespace GlassIsland
{
#if UNITY_EDITOR
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
#endif
}
