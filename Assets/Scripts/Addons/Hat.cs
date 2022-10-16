using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "Custom/Hat", order = 0)]
public class Hat : ScriptableObject
{
    public int Id;
    public Sprite Sprite;
    public GameObject Prefab;
    public int Cost;
    public bool IsUnlocked;
}
