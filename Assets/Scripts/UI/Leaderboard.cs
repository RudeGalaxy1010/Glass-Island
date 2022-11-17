using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GlassIsland.UI
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private LeaderBoard.LeaderBoard _leaderBoard;

        private void OnEnable()
        {
            _leaderBoard.DataUpdated += OnDataUpdated;
        }

        private void OnDisable()
        {
            _leaderBoard.DataUpdated -= OnDataUpdated;
        }

        private void OnDataUpdated()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            _rank.text = _leaderBoard.Rank.ToString();
            _score.text = _leaderBoard.Score.ToString();
            Debug.Log("Update UI!");
            Debug.Log(_leaderBoard.Rank);
            Debug.Log(_leaderBoard.Score);
        }
    }
}
