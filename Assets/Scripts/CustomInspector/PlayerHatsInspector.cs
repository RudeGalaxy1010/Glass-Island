using UnityEditor;
using UnityEngine;

namespace GlassIsland
{
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
}
