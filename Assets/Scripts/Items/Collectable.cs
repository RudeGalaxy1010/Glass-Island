using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public abstract void Init(int value);
    public abstract void PickUp(Player player);
}
