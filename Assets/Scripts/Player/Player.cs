using UnityEngine;

namespace GlassIsland
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BrickStack[] _bricks;

        private int _score;
        private int _bricksCount;

        public void AddBricks(int value)
        {
            _bricksCount += value;
            UpdateStorage(_bricksCount);
        }

        private void UpdateStorage(int bricksCount)
        {
            foreach (var brick in _bricks)
            {
                brick.gameObject.SetActive(false);
            }

            int bricksToShow = bricksCount < _bricks.Length ? bricksCount : _bricks.Length;

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
                UpdateStorage(_bricksCount);
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
