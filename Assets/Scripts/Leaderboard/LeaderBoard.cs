using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace GlassIsland.LeaderBoard
{
    public class LeaderBoard : MonoBehaviour
    {
        private const string BoardName = "Money";

        public event UnityAction NewRecordAdded;

        [SerializeField] private Money _money;

        private int _score;
        private int _rank;
        private string _playerName;

        private void OnEnable()
        {
            _money.MoneyAdded += OnMoneyAdded;
        }

        private void OnDisable()
        {
            _money.MoneyAdded -= OnMoneyAdded;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            if (PlayerAccount.IsAuthorized == false)
            {
                PlayerAccount.Authorize();
            }
        }

        public int Rank => _rank;
        public int Score => _score;
        public string PlayerName => _playerName;

        public void UpdateData()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.GetPlayerEntry(BoardName, (result) =>
            {
                if (result == null)
                {
                    _rank = 0;
                    _score = 0;
                    _playerName = "-";
                }
                else
                {
                    Debug.Log(result);
                    _rank = result.rank;
                    _score = result.score;

                    if (string.IsNullOrEmpty(result.player.publicName))
                    {
                        _playerName = "-";
                    }
                    else
                    {
                        _playerName = result.player.publicName;
                    }
                }
            });
        }

        private void OnMoneyAdded(int value)
        {
            int score = 0;

            Leaderboard.GetEntries(BoardName, (result) =>
            {
                if (result == null)
                {
                    SetScore(0);
                    return;
                }

                score = result.entries.Max(e => e.score);
            });

            score += value;
            SetScore(score);
        }

        private void SetScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.SetScore(BoardName, score);
            NewRecordAdded?.Invoke();
        }
    }
}
