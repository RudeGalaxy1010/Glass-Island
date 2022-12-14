using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(CharacterController))]
    public class Move : MonoBehaviour
    {
        private const float GroundDistance = 0.15f;
        private const float GroundPinForce = -2f;
        private const float JumpConstant = -2f;

        private const float IdleAnimatorSpeed = 1;
        private const float MaxDirectionVectorLength = 1.41f;

        [SerializeField] private PlayerProperties _playerUpgrade;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravityScale;
        [SerializeField] private Animator _animator;
        [SerializeField] private InputBase _input;
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
            _jumpVelocity = GetJumpVelovity(_jumpHeight);
            _isJumped = true;
        }

        private void Update()
        {
            MoveByInput();
            CheckGround();
            ApplyGravitation();
            TryJump();
            Rotate();
        }

        private float Speed => _playerUpgrade.Speed;
        private float GetJumpVelovity(float height) => Mathf.Sqrt(height * JumpConstant * (Physics.gravity.y * _gravityScale));

        public void Enable()
        {
            _controller.enabled = true;
            ResetAnimator();
            enabled = true;
        }

        public void Disable()
        {
            _controller.enabled = false;
            ResetAnimator();
            enabled = false;
        }

        public void Jump(float height)
        {
            float velocity = GetJumpVelovity(height);
            _animator.SetTrigger(PlayerAnimatorConstants.JumpAnimation);
            _verticalVelocity = velocity;
            _isJumped = true;
        }

        private void ResetAnimator()
        {
            _animator.SetBool(PlayerAnimatorConstants.RunningAnimation, false);
        }

        private void MoveByInput()
        {
            Vector3 moveDirection = _input.Velocity;

            if (moveDirection.magnitude > 0)
            {
                _animator.SetBool(PlayerAnimatorConstants.RunningAnimation, true);
                _animator.speed = moveDirection.magnitude / MaxDirectionVectorLength;
            }
            else
            {
                _animator.SetBool(PlayerAnimatorConstants.RunningAnimation, false);
                _animator.speed = IdleAnimatorSpeed;
            }

            _controller.Move((moveDirection * Speed + Vector3.up * _verticalVelocity) * Time.deltaTime);
        }

        private void TryJump()
        {
            if (_isJumped == true)
            {
                return;
            }

            if (_isGrounded == true)
            {
                _isJumped = false;
                return;
            }

            if (_isGrounded == false && _input.Velocity.magnitude > 0)
            {
                _animator.SetTrigger(PlayerAnimatorConstants.JumpAnimation);
                _verticalVelocity = _jumpVelocity;
                _isJumped = true;
            }
        }

        private void Rotate()
        {
            Vector3 lookDirection = _input.Velocity * Speed * Time.deltaTime;

            if (lookDirection.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }

        private void CheckGround()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, GroundDistance, _groundMask);
        }

        private void ApplyGravitation()
        {
            if (_isGrounded && _verticalVelocity < GroundPinForce)
            {
                _verticalVelocity = GroundPinForce;
                return;
            }

            _verticalVelocity += Physics.gravity.y * _gravityScale * Time.deltaTime;
        }
    }
}
