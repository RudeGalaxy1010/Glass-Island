using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Character))]
    public class DieCondition : MonoBehaviour
    {
        private const float MinHeighth = -10;
        private Character _character;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void Update()
        {
            if (transform.position.y <= MinHeighth)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            _character.Die();
        }
    }
}
