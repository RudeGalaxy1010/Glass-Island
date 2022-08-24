using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private Dissolvable _dissolvingBody;
        [SerializeField] private List<Dissolvable> _dissolvableItems;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;
        [SerializeField] private bool _needFadeByFirstPress;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;

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

        public void Press(Player player)
        {
            _targetPosition = _pressedPosition;

            if ((IsDissolved || _isDissolving) && player.TrySubtractBrick())
            {
                _dissolvingBody.Appear();
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
            _isDissolving = false;
            Dissolved?.Invoke();
        }

        private void Move()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            }
        }

        private void RemoveCollectable(Dissolvable dissolvable)
        {
            dissolvable.Dissolved -= RemoveCollectable;
            _dissolvableItems.Remove(dissolvable);
        }
    }
}
