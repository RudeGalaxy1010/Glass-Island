using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : Dissolvable
    {
        public abstract void Init(int value);
        public abstract void PickUp(Character character);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                PickUp(character);
                Appear();
                FinishDissolving(true);
            }
        }
    }
}
