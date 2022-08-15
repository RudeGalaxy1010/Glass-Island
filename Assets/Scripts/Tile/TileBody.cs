using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private GameObject[] _dissolvingParts;
        [SerializeField] private Vector3 _shift;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;

        private Vector3 _idlePosition;
        private Vector3 _pressedPosition;
        private Vector3 _targetPosition;

        private float _timer;
        private bool _isDissolving;

        private Material[] _materials;
        private float[] _maxAlphas;

        public bool IsDissolved => _dissolvingParts[0].activeSelf == false;

        private void Start()
        {
            _idlePosition = transform.position;
            _pressedPosition = _idlePosition + _shift;
            _targetPosition = _idlePosition;

            _materials = new Material[_dissolvingParts.Length];
            _maxAlphas = new float[_dissolvingParts.Length];

            for (int i = 0; i < _dissolvingParts.Length; i++)
            {
                _materials[i] = _dissolvingParts[i].GetComponent<Renderer>().material;
                _maxAlphas[i] = _materials[i].color.a;
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
                for (int i = 0; i < _materials.Length; i++)
                {
                    _materials[i].color = new Color(_materials[i].color.r, _materials[i].color.g, _materials[i].color.b, _maxAlphas[i]);
                }

                foreach (var dissolvingPart in _dissolvingParts)
                {
                    dissolvingPart.SetActive(true);
                }

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

            for (int i = 0; i < _materials.Length; i++)
            {
                float materialAlpha = Mathf.Min(_timer / _extinctTime, _maxAlphas[i]);
                _materials[i].color = new Color(_materials[i].color.r, _materials[i].color.g, _materials[i].color.b, materialAlpha);
            }

            if (_timer <= 0)
            {
                foreach (var dissolvingPart in _dissolvingParts)
                {
                    dissolvingPart.SetActive(false);
                }

                _isDissolving = false;
                Dissolved?.Invoke();
            }
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
            _timer = _extinctTime + _extinctDelay;
        }
    }
}
