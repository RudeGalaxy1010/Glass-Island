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

            if (_brickStacksCount < _bricks.Length)
            {
                _bricks[_brickStacksCount].gameObject.SetActive(true);
            }
        }

        public void AddScore(int value)
        {
            _score += value;
        }
    }
}
