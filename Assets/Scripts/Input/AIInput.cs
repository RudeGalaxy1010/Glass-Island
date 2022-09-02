using UnityEngine;

namespace GlassIsland
{
    public class AIInput : InputBase
    {
        private const float ReachDistance = 0.15f;
        private const float MinDistanceToDestination = 2f;

        [SerializeField] private Vector2 _limitsX;
        [SerializeField] private Vector2 _limitsZ;

        private Vector3 _destinationPoint;

        private void Start()
        {
            _destinationPoint = transform.position;
        }

        private void Update()
        {
            CheckReachDestination();
            UpdateVelocity();
        }

        private void UpdateVelocity()
        {
            Vector3 direction = _destinationPoint - transform.position;
            InputVelocity = new Vector3(direction.normalized.x, 0, direction.normalized.z);
        }

        private void CheckReachDestination()
        {
            if (Velocity.magnitude <= ReachDistance)
            {
                ChangeDestinationPoint();
            }
        }

        private void ChangeDestinationPoint()
        {
            while ((_destinationPoint - transform.position).magnitude < MinDistanceToDestination)
            {
                _destinationPoint = new Vector3(Random.Range(_limitsX.x, _limitsX.y), 0, Random.Range(_limitsZ.x, _limitsZ.y));
            }
        }
    }
}
