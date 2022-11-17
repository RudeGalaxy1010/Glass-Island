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

            UpdateData();
        }

        public int Rank => _rank;
        public int Score => _score;

        public void UpdateData()
        {
            Debug.Log("Load data from LB!");
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.GetPlayerEntry(BoardName, (result) =>
            {
                Debug.Log("Got result from LB!");
                if (result == null)
                {
                    _rank = 0;
                    _score = 0;
                    SetScore(0);
                    Debug.Log("Result from LB == null!");
                }
                else
                {
                    _rank = result.rank;
                    _score = result.score;
                    Debug.Log($"Result from LB: {result.rank} | {result.score}");
                    Debug.Log($"Result from LB: {_rank} | {_score}");
                }

                DataUpdated?.Invoke();
            });
        }

        private void OnMoneyAdded(int value)
        {
            Debug.Log("Money added! Update LB!");
            UpdateData();
            int score = _score + value;
            SetScore(score);
        }

        private void SetScore(int score)
        {
            Debug.Log($"Set score to {score}, update LB!");
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            Leaderboard.SetScore(BoardName, score);
            UpdateData();
        }
    }
}
