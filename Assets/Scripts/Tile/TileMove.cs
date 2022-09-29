using System;
using UnityEngine;

namespace GlassIsland
{
    public class TileMove : MonoBehaviour
    {
        [SerializeField] private TileButton _tileButton;
        //[SerializeField] private TileBody _tileBody;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _downSpeed;
        [SerializeField] private float _upSpeed;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private void OnEnable()
        {
            //_tileBody.Pressed += OnPress;
            //_tileBody.Unpressed += OnUnpress;
            //_tileBody.Dissolved += OnDissolved;

            _tileButton.Pressed += OnPress;
            _tileButton.Unpressed += OnUnpress;
        }

        private void OnDisable()
        {
            //_tileBody.Pressed -= OnPress;
            //_tileBody.Unpressed -= OnUnpress;
            //_tileBody.Dissolved -= OnDissolved;

            _tileButton.Pressed -= OnPress;
            _tileButton.Unpressed -= OnUnpress;
        }

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float speed = _targetPosition == _pressedPosition ? _downSpeed : _upSpeed;

            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
            }
        }

        private void OnPress(Character character)
        {
            _targetPosition = _pressedPosition;
        }

        private void OnUnpress()
        {
            _targetPosition = _idlePosition;
        }

        private void OnDissolved()
        {
            transform.position = _idlePosition;
        }
    }
}
