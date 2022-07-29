using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _speed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private Collider _tileColldier;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private Material _tileMaterial;
        private bool _isPressed;
        private float _timer;

        private void Start()
        {
            _tileMaterial = _tileColldier.GetComponent<Renderer>().material;
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (_isPressed == true)
                {
                    return;
                }

                _targetPosition = _pressedPosition;
                StartDissolving();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _targetPosition = _idlePosition;
            }
        }

        private void Update()
        {
            if (_isPressed == true)
            {
                Dissolve();
            }

            Move();
        }

        private void Move()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            }
        }

        private void Dissolve()
        {
            _timer += Time.deltaTime;

            float materialAlpha = 1 - (_timer / _extinctTime);
            _tileMaterial.color = new Color(_tileMaterial.color.r, _tileMaterial.color.g, _tileMaterial.color.b, materialAlpha);

            if (_timer >= _extinctTime)
            {
                _tileColldier.gameObject.SetActive(false);
                _isPressed = false;
            }
        }

        private void StartDissolving()
        {
            _isPressed = true;
            _timer = 0;
        }
    }
}
