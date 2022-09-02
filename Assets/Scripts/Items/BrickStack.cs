using UnityEngine;

namespace GlassIsland
{
    public class BrickStack : Collectable
    {
        [SerializeField] private Canvas _countTextCanvas;
        [SerializeField] private GameObject _additionalBrick;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _rotationSpeed;

        private Transform _target;
        private int _count;
        private Character _character;
        private bool _isPickedUp;

        public override void Init(int bricksCount)
        {
            _count = bricksCount;
            UpdateCountText();
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.position, _movingSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _target.rotation, _rotationSpeed * Time.deltaTime);

            if (transform.position == _target.position)
            {
                gameObject.SetActive(false);
                _character.AddBricks(_count);
            }
        }

        public override void PickUp(Character character)
        {
            if (_isPickedUp == true)
            {
                return;
            }

            _isPickedUp = true;
            _character = character;
            transform.SetParent(null);
            _target = character.LastBrickPoint;
            _countTextCanvas.gameObject.SetActive(false);
        }

        private void UpdateCountText()
        {
            if (_count == 5)
            {
                _countTextCanvas.gameObject.SetActive(true);
                _additionalBrick.SetActive(true);
            }
            else
            {
                _countTextCanvas.gameObject.SetActive(false);
                _additionalBrick.SetActive(false);
            }
        }
    }
}
