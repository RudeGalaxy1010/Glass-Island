using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private Dissolvable _dissolvingBody;
        [SerializeField] private List<Dissolvable> _dissolvableItems;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _downSpeed;
        [SerializeField] private float _upSpeed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;
        [SerializeField] private bool _needFadeByFirstPress;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;
        private Collider _collider;

        public bool IsDissolved => _dissolvingBody.gameObject.activeSelf == false;

        private void OnEnable()
        {
            foreach (var dissolvingCollectable in _dissolvableItems)
            {
                dissolvingCollectable.Dissolved += RemoveCollectable;
            }
        }

        private void OnDisable()
        {
            foreach (var dissolvingCollectable in _dissolvableItems)
            {
                dissolvingCollectable.Dissolved -= RemoveCollectable;
            }
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;
        }

        private void Update()
        {
            if (_isDissolving == true)
            {
                Dissolve();
            }

            Move();
        }

        public void Press(Character character)
        {
            _targetPosition = _pressedPosition;

            if ((IsDissolved || _isDissolving) && character.TrySubtractBrick())
            {
                _dissolvingBody.Appear();
                _collider.enabled = true;
                _isDissolving = false;
                
                if (_needFadeByFirstPress == false)
                {
                    return;
                }
            }

            _isDissolving = true;
            _timer = _extinctTime / 2f + _extinctDelay;
        }

        public void Unpress()
        {
            _targetPosition = _idlePosition;
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;
            _dissolvingBody.SetAlpha(_timer / _extinctTime);

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(_timer / _extinctTime);
            }

            if (_timer <= -_extinctTime)
            {
                FinishDissolving();
            }
        }

        private void FinishDissolving()
        {
            int dissolvableCount = _dissolvableItems.Count;

            for (int i = 0; i < dissolvableCount; i++)
            {
                _dissolvableItems[i].FinishDissolving();

                if (dissolvableCount != _dissolvableItems.Count)
                {
                    dissolvableCount = _dissolvableItems.Count;
                    i--;
                }
            }

            _dissolvingBody.FinishDissolving();
            _collider.enabled = false;
            _isDissolving = false;
            Dissolved?.Invoke();
        }

        private void Move()
        {
            float speed = _targetPosition == _pressedPosition ? _downSpeed : _upSpeed;

            if (_dissolvingBody.transform.position != _targetPosition)
            {
                _dissolvingBody.transform.position = Vector3.MoveTowards(_dissolvingBody.transform.position, _targetPosition, speed * Time.deltaTime);
            }
        }

        private void RemoveCollectable(Dissolvable dissolvable)
        {
            dissolvable.Dissolved -= RemoveCollectable;
            _dissolvableItems.Remove(dissolvable);
        }
    }
}
