using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        private const float GroundDistance = 0.2f;
        private const float GroundPinForce = -2f;

        private const float IdleAnimatorSpeed = 1;
        private const float MaxDirectionVectorLength = 1.41f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;

        private bool _isGrounded;
        private Vector3 _gravitationVelocity;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
            CheckGround();
            ApplyGravitation();
            Rotate();
        }

        public void Enable()
        {
            _controller.enabled = true;
            enabled = true;
        }

        public void Disable()
        {
            _controller.enabled = false;
            enabled = false;
        }

        private void Move()
        {
            if (_joystick.Direction.magnitude > 0)
            {
                var moveDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
                _controller.Move(moveDirection * _speed * Time.deltaTime);
                _animator.SetBool(PlayerAnimatorConstants.RunningAnimation, true);
                _animator.speed = moveDirection.magnitude / MaxDirectionVectorLength;
            }
            else
            {
                _animator.SetBool(PlayerAnimatorConstants.RunningAnimation, false);
                _animator.speed = IdleAnimatorSpeed;
            }
        }

        private void Rotate()
        {
            if (_joystick.Direction.magnitude > 0)
            {
                var lookDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
                transform.rotation = Quaternion.LookRotation(lookDirection * _speed * Time.deltaTime);
            }
        }

        private void CheckGround()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, GroundDistance, _groundMask);
        }

        private void ApplyGravitation()
        {
            if (_isGrounded && _gravitationVelocity.y < 0)
            {
                _gravitationVelocity.y = GroundPinForce;
                return;
            }

            _gravitationVelocity.y += Physics.gravity.y * Time.deltaTime;
            _controller.Move(_gravitationVelocity * Time.deltaTime);
        }
    }
}
