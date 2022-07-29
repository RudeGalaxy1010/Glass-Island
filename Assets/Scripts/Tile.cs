using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _speed;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _targetPosition = _pressedPosition;
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
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            }
        }
    }
}
