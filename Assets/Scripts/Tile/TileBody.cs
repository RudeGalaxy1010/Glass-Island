using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private GameObject _hexagon;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _extinctTime;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;

        private Material _material;

        public bool IsDissolved => _hexagon.activeSelf == false;

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
            _material = _hexagon.GetComponent<Renderer>().material;
        }

        private void Update()
        {
            if (_isDissolving == true)
            {
                Dissolve();
            }

            Move();
        }

        public void Press(Player player)
        {
            _targetPosition = _pressedPosition;

            if (IsDissolved == false && _isDissolving == false)
            {
                StartDissolving();
                return;
            }

            if (player.TrySubtractBrick())
            {
                _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1);
                _hexagon.SetActive(true);
                _isDissolving = false;
            }
        }

        public void Unpress()
        {
            _targetPosition = _idlePosition;
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;

            float materialAlpha = _timer / _extinctTime;
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, materialAlpha);

            if (_timer <= 0)
            {
                _hexagon.SetActive(false);
                _isDissolving = false;
                Dissolved?.Invoke();
            }
        }

        private void Move()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            }
        }

        private void StartDissolving()
        {
            _isDissolving = true;
            _timer = _extinctTime;
        }
    }
}
