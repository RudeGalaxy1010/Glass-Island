using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _speed;
        [SerializeField] private float _extinctTime;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;

        private Material _material;

        private void OnEnable()
        {
            _tileButton.Pressed += Press;
            _tileButton.Pressed += StartDissolving;
            _tileButton.Unpressed += Unpress;
        }

        private void OnDisable()
        {
            _tileButton.Pressed -= Press;
            _tileButton.Pressed -= StartDissolving;
            _tileButton.Unpressed -= Unpress;
        }

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
            _material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            if (_isDissolving == true)
            {
                Dissolve();
            }

            Move();
        }

        private void Press()
        {
            _targetPosition = _pressedPosition;
        }

        private void Unpress()
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
                gameObject.SetActive(false);
                _isDissolving = false;
                Dissolved?.Invoke();
            }
        }

        private void Move()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            }
        }

        private void StartDissolving()
        {
            if (_isDissolving == true)
            {
                return;
            }

            _isDissolving = true;
            _timer = _extinctTime;
        }
    }
}
