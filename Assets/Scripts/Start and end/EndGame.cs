using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Player))]
    public class EndGame : DieCondition
    {
        [SerializeField] private float _minHeight;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private SmoothFollow _smoothFollow;

        private Move _move;

        private void Start()
        {
            _move = GetComponent<Move>();
        }

        protected override void Die()
        {
            _move.enabled = false;
            _smoothFollow.SetDefaultTarget();
            _endScreen.SetActive(true);
            base.Die();
        }
    }
}
