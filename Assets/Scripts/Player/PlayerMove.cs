using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _speed;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
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
                _characterController.Move(moveDirection * _speed * Time.deltaTime);
                _animator.SetBool("IsRunning", true);
            }
            else
            {
                _animator.SetBool("IsRunning", false);
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
