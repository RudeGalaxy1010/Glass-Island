using UnityEditor;
using UnityEngine;

namespace GlassIsland
{
    [CustomEditor(typeof(Money))]
    public class MoneyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Reset money"))
            {
                ((Money)target).ResetBalance();
                Debug.Log("Money reseted");
            }
        }
    }
}
