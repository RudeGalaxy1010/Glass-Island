using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class Character : MonoBehaviour
    {
        public event UnityAction<Character> Died;

        [SerializeField] private string _name;
        [SerializeField] private TMP_Text _nameText;

        [SerializeField] private GameObject[] _bricks;
        [SerializeField] private float _brickHeight;
        [SerializeField] private Transform _bricksTargetPoint;
        [SerializeField] private Outline _outline;

        private int _score;
        private int _bricksCount;

        public Transform LastBrickPoint => _bricksTargetPoint;
        public int Score => _score;

        private void Start()
        {
            _nameText.text = _name;
        }

        public void AddBricks(int value)
        {
            _bricksCount += value;
            UpdateStorage(_bricksCount);
        }

        public void AddScore(int value)
        {
            _score += value;
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

        public void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        public void DisableOutline()
        {
            _outline.enabled = false;
        }
    }
}
