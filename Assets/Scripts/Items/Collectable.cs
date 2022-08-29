using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : Dissolvable
    {
        public abstract void Init(int value);
        public abstract void PickUp(Player player);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                PickUp(player);
                Appear();
                FinishDissolving(true);
            }
        }
    }
}
