using UnityEditor;
using UnityEngine;

namespace GlassIsland
{
#if UNITY_EDITOR
    [CustomEditor(typeof(HatProperties))]
    public class PlayerHatsInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Reset hats"))
            {
                ((HatProperties)target).ResetHats();
                Debug.Log("Hats reseted");
            }
        }
    }
#endif
}
