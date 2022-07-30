using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PickUp(player);
        }
    }

    protected abstract void PickUp(Player player);
}
