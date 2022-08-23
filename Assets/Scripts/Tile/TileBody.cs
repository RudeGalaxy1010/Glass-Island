using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private GameObject _dissolvingBody;
        [SerializeField] private List<Collectable> _dissolvingCollectables;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;

        private Material _bodyMaterial;
        private List<Material> _materials;
        private float _maxBodyAlpha;
        private List<float> _maxAlphas;

        public bool IsDissolved => _dissolvingBody.activeSelf == false;

        private void OnEnable()
        {
            foreach (var dissolvingCollectable in _dissolvingCollectables)
            {
                dissolvingCollectable.PickedUp += RemoveCollectable;
            }
        }

        private void OnDisable()
        {
            foreach (var dissolvingCollectable in _dissolvingCollectables)
            {
                dissolvingCollectable.PickedUp -= RemoveCollectable;
            }
        }

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;

            _bodyMaterial = _dissolvingBody.GetComponent<Renderer>().material;
            _materials = new List<Material>();
            _maxBodyAlpha = _bodyMaterial.color.a;
            _maxAlphas = new List<float>();

            for (int i = 0; i < _dissolvingCollectables.Count; i++)
            {
                _materials.Add(_dissolvingCollectables[i].GetComponentInChildren<Renderer>().material);
                _maxAlphas.Add(_materials[i].color.a);
            }
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

            if (IsDissolved == false && _isDissolving == false)
            {
                StartDissolving();
                return;
            }

            if (player.TrySubtractBrick())
            {
                _dissolvingBody.SetActive(true);
                _bodyMaterial.color = new Color(_bodyMaterial.color.r, _bodyMaterial.color.g, _bodyMaterial.color.b, _maxBodyAlpha);
                _isDissolving = false;
            }
        }

        public void Unpress()
        {
            _targetPosition = _idlePosition;
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;

            for (int i = 0; i < _materials.Count; i++)
            {
                float materialAlpha = Mathf.Min(_timer / _extinctTime, _maxAlphas[i]);
                _materials[i].color = new Color(_materials[i].color.r, _materials[i].color.g, _materials[i].color.b, materialAlpha);
            }

            float bodyAlpha = Mathf.Min(_timer / _extinctTime, _maxBodyAlpha);
            _bodyMaterial.color = new Color(_bodyMaterial.color.r, _bodyMaterial.color.g, _bodyMaterial.color.b, bodyAlpha);

            if (_timer <= -_extinctTime)
            {
                FinishDissolving();
            }
        }

        private void FinishDissolving()
        {
            foreach (var dissolvingPart in _dissolvingCollectables)
            {
                dissolvingPart.gameObject.SetActive(false);
            }

            _dissolvingBody.SetActive(false);
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

        private void StartDissolving()
        {
            _isDissolving = true;
            _timer = _extinctTime / 2f + _extinctDelay;
        }

        private void RemoveCollectable(Collectable collectable)
        {
            collectable.PickedUp -= RemoveCollectable;
            _dissolvingCollectables.Remove(collectable);
        }
    }
}
