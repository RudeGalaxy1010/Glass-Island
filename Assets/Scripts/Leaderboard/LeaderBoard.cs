using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland.LeaderBoard
{
    public class LeaderBoard : MonoBehaviour
    {
        private const string BoardName = "Money";

        public event UnityAction DataUpdated;

        [SerializeField] private Money _money;

        private int _score;
        private int _rank;

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

        public void UpdateData()
        {
            if (YandexGamesSdk.IsInitialized == false || PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.GetPlayerEntry(BoardName, (result) =>
            {
                if (result == null)
                {
                    _rank = 0;
                    _score = 0;
                    SetScore(0);
                }
                else
                {
                    _rank = result.rank;
                    _score = result.score;
                }

                DataUpdated?.Invoke();
            });
        }

        private void OnMoneyAdded(int value)
        {
            int score = _score + value;
            SetScore(score);
        }

        private void SetScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.SetScore(BoardName, score);
        }
    }
}
