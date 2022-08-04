using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GlassIsland
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _scoreText;

        private void OnEnable()
        {
            _player.ScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            _player.ScoreChanged -= UpdateScoreText;
        }

        private void UpdateScoreText(int value)
        {
            _scoreText.text = value.ToString();
        }
    }
}
