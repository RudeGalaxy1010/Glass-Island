using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Player))]
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private float _minHeight;
        [SerializeField] private SmoothFollow _smoothFollow;

        private Player _player;
        private PlayerMove _playerMove;

        private void Start()
        {
            _player = GetComponent<Player>();
            _playerMove = GetComponent<PlayerMove>();
        }

        private void Update()
        {
            if (transform.position.y <= _minHeight)
            {
                _playerMove.enabled = false;
                _smoothFollow.SetDefaultTarget();
                _player.Die();
            }
        }
    }
}
