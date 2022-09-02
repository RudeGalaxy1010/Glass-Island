using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Player))]
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private float _minHeight;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private SmoothFollow _smoothFollow;

        private Player _player;
        private Move _playerMove;

        private void Start()
        {
            _player = GetComponent<Player>();
            _playerMove = GetComponent<Move>();
        }

        private void Update()
        {
            if (transform.position.y <= _minHeight)
            {
                _playerMove.enabled = false;
                _smoothFollow.SetDefaultTarget();
                _endScreen.SetActive(true);
                _player.Die();
            }
        }
    }
}
