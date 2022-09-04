using UnityEngine;

namespace GlassIsland
{
    public class AIInput : InputBase
    {
        private const float ReachDistance = 0.15f;
        private const float MinDestinationDistance = 2f;

        [SerializeField] private Vector2 _limitsX;
        [SerializeField] private Vector2 _limitsZ;
        [SerializeField] private float _stackProcessTime;

        private Vector3 _destinationPoint;
        private Vector3 _stackPosition;
        private float _stackProcessTimer;

        private void Start()
        {
            _destinationPoint = transform.position;
        }

        private void Update()
        {
            CheckReachDestination();
            ProcessStack();
        }

        private void ProcessStack()
        {
            _stackProcessTimer -= Time.deltaTime;

            if (_stackProcessTimer <= 0)
            {
                _stackProcessTimer = _stackProcessTime;

                if ((_stackPosition - transform.position).magnitude < ReachDistance)
                {
                    ChangeDestinationPoint();
                }

                _stackPosition = transform.position;
            }
        }

        private void UpdateVelocity()
        {
            Vector3 direction = _destinationPoint - new Vector3(transform.position.x, 0, transform.position.z);
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
            _destinationPoint = new Vector3(Random.Range(_limitsX.x, _limitsX.y), 0, Random.Range(_limitsZ.x, _limitsZ.y));
            UpdateVelocity();
        }
    }
}
