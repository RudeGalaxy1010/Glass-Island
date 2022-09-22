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

        [Space(30)]
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _downSpeed;
        [SerializeField] private float _upSpeed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;
        [SerializeField] private float _appearTime;
        [SerializeField] private bool _needFadeByFirstPress;

        [Space(30)]
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _dissolveMaterial;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _needAppearing;
        private bool _needDissolving;

        private Renderer _renderer;

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
            _renderer = _dissolvingBody.GetComponent<Renderer>();
        }

        private void Update()
        {
            if (_needAppearing == true)
            {
                Appear();
            }
            else if (_needDissolving == true)
            {
                Dissolve();
            }

            Move();
        }

        public void Press(Character character)
        {
            _targetPosition = _pressedPosition;

            if ((IsDissolved || _needDissolving) && character.TrySubtractBrick())
            {
                gameObject.SetActive(true);
                _dissolvingBody.gameObject.SetActive(true);
                _needAppearing = true;
                _needDissolving = false;
                _timer = 0;

                if (_needFadeByFirstPress == false)
                {
                    return;
                }
            }

            _needDissolving = true;
            _timer = _extinctTime / 2f + _extinctDelay;
        }

        public void Unpress()
        {
            _targetPosition = _idlePosition;
        }

        private void Appear()
        {
            if (_timer / _appearTime > 1)
            {
                _needAppearing = false;
                return;
            }

            _timer += Time.deltaTime;
            _dissolvingBody.SetAlpha(_timer / _appearTime);
            _renderer.material = _defaultMaterial;

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(_timer / _appearTime);
            }
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;
            _dissolvingBody.SetAlpha(_timer / _extinctTime);
            _renderer.material = _dissolveMaterial;

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
            gameObject.SetActive(false);
            _needDissolving = false;
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
