using System.Collections;
using TMPro;
using UnityEngine;

namespace GlassIsland
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _scoreCalcDuration;

        private void OnEnable()
        {
            StartCoroutine(UpdateScoreText(_player.Score));
        }

        private IEnumerator UpdateScoreText(int value)
        {
            for (int i = 0; i < value; i++)
            {
                _scoreText.text = $"+{i}";
                yield return new WaitForSeconds(_scoreCalcDuration / value);
            }

            _scoreText.text = $"+{value}";
        }
    }
}
