using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        private const float IdleAnimatorSpeed = 1;
        private const float MaxDirectionVectorLength = 1.41f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _speed;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            if (_joystick.Direction.magnitude > 0)
            {
                var moveDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
                _rigidbody.MovePosition(transform.position + moveDirection * _speed * Time.deltaTime);
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
    }
}
