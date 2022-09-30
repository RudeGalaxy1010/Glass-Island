using System;
using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Custom/Levels", order = 0)]
    public class Levels : ScriptableObject
    {
        public List<Level> List;
    }

    [Serializable]
    public class Level
    {
        public GameObject MapPrefab;
        public float SpawnHeight;
        public float MinDieHeight;
    }
}