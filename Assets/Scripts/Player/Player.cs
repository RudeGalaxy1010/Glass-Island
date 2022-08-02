using TMPro;
using UnityEngine;

namespace GlassIsland
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private BrickStack[] _bricks;
        [SerializeField] private TMP_Text _nameText;

        private int _score;
        private int _bricksCount;

        private void Start()
        {
            _nameText.text = _name;
        }

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

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
