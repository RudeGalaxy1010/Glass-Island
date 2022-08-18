using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class Player : MonoBehaviour
    {
        public event UnityAction<int> ScoreChanged;

        [SerializeField] private string _name;
        [SerializeField] private GameObject[] _bricks;
        [SerializeField] private float _brickHeight;
        [SerializeField] private Transform _bricksTargetPoint;
        [SerializeField] private TMP_Text _nameText;

        private int _score;
        private int _bricksCount;

        public Transform LastBrickPoint => _bricksTargetPoint;

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
                brick.SetActive(false);
            }

            int bricksToShow = bricksCount < _bricks.Length ? bricksCount : _bricks.Length;

            for (int i = 0; i < bricksToShow; i++)
            {
                _bricks[i].SetActive(true);
                _bricksTargetPoint.position = _bricks[i].transform.position + _bricks[i].transform.up * _brickHeight;
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
            ScoreChanged?.Invoke(_score);
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
