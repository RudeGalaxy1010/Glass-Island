using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        private const float GroundDistance = 0.15f;
        private const float GroundPinForce = -1f;

        private const float IdleAnimatorSpeed = 1;
        private const float MaxDirectionVectorLength = 1.41f;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravityScale;
        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;

        private bool _isGrounded;
        private bool _isJumped;
        private float _jumpVelocity;
        private float _verticalVelocity;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _jumpVelocity = Mathf.Sqrt(_jumpHeight * -2f * (Physics.gravity.y * _gravityScale));
            _isJumped = true;
        }

        private void Update()
        {
            Move();
            CheckGround();
            ApplyGravitation();
            TryJump();
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

            _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }

        private void TryJump()
        {
            if (_isGrounded == true)
            {
                _isJumped = false;
                return;
            }

            if (_isJumped == true)
            {
                return;
            }

            if (_isGrounded == false && _joystick.Direction.magnitude > 0)
            {
                _animator.SetTrigger(PlayerAnimatorConstants.JumpAnimation);
                _verticalVelocity = _jumpVelocity;
                _isJumped = true;
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
            if (_isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = GroundPinForce;
                return;
            }

            _verticalVelocity += Physics.gravity.y * _gravityScale * Time.deltaTime;
        }
    }
}
