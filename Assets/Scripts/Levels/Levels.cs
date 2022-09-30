using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "Levels", order = 0)]
public class Levels : ScriptableObject
{
    public List<Level> List;
}

[Serializable]
public class Level
{
    public GameObject MapPrefab;
}
