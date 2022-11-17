using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GlassIsland.UI
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private LeaderBoard.LeaderBoard _leaderBoard;

        private void OnEnable()
        {
            _leaderBoard.NewRecordAdded += OnNewRecordAdded;
        }

        private void OnDisable()
        {
            _leaderBoard.NewRecordAdded -= OnNewRecordAdded;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);
            UpdateData();
        }

        private void OnNewRecordAdded()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            _leaderBoard.UpdateData();
            _name.text = _leaderBoard.PlayerName;
            _rank.text = _leaderBoard.Rank.ToString();
            _score.text = _leaderBoard.Score.ToString();
        }
    }
}
