using UnityEngine;

namespace GlassIsland
{
    public class BrickStack : Collectable
    {
        [SerializeField] private Canvas _countTextCanvas;
        [SerializeField] private float _speed;

        private Vector3 _targetPosition;
        private int _count;

        public override void Init(int bricksCount)
        {
            _count = bricksCount;
            _targetPosition = transform.position;
            UpdateCountText();
        }

        private void Update()
        {
            if (transform.position == _targetPosition)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

        public override void PickUp(Player player)
        {
            player.AddBricks(_count);
            transform.SetParent(player.transform);
            _targetPosition = player.GetBrickPosition(this);
        }

        private void UpdateCountText()
        {
            if (_count == 5)
            {
                _countTextCanvas.gameObject.SetActive(true);
            }
            else
            {
                _countTextCanvas.gameObject.SetActive(false);
            }
        }
    }
}
