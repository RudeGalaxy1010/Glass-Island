using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Character))]
    public class DieCondition : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;

        private Character _character;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void Update()
        {
            if (transform.position.y <= _levelManager.CurrentLevel.MinDieHeight)
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
