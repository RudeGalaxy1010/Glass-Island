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
            }
        }

        public override void PickUp(Player player)
        {
            player.AddBricks(_count);
            transform.SetParent(null);
            _target = player.LastBrickPoint;
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
