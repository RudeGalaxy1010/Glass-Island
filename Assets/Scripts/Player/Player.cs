using UnityEngine;

namespace GlassIsland
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BrickStack[] _bricks;

        private int _score;
        private int _bricksCount;
        private int _brickStacksCount;

        public void AddBricks(int value)
        {
            _bricksCount += value;
            _brickStacksCount++;
            UpdateStorage();
        }

        private void UpdateStorage()
        {
            foreach (var brick in _bricks)
            {
                brick.gameObject.SetActive(false);
            }

            int bricksToShow = _brickStacksCount < _bricks.Length ? _brickStacksCount : _bricks.Length;

            for (int i = 0; i < bricksToShow; i++)
            {
                _bricks[i].gameObject.SetActive(true);
            }
        }

        public bool TrySubtractBrick()
        {
            if (_bricksCount > 0)
            {
                _bricksCount--;

                if (_brickStacksCount <= 0 && _bricksCount > 0)
                {
                    _brickStacksCount = 1;
                }
                else if (_bricksCount == 0)
                {
                    _brickStacksCount = 0;
                }
                else if (_brickStacksCount > 1)
                {
                    _brickStacksCount--;
                }

                UpdateStorage();
                return true;
            }

            return false;
        }

        public void AddScore(int value)
        {
            _score += value;
        }
    }
}
