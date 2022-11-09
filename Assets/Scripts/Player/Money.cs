using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    public class Money : MonoBehaviour
    {
        private const string SaveKey = "Money";

        public event UnityAction<int> MoneyChanged;

        [SerializeField] private WinGame _winGame;
        [SerializeField] private LoseGame _loseGame;
        [SerializeField] private Player _player;

        private int _balance;

        private void OnEnable()
        {
            _winGame.Won += OnGameWon;
            _loseGame.Lost += OnGameLost;
        }

        private void OnDisable()
        {
            _winGame.Won -= OnGameWon;
            _loseGame.Lost -= OnGameLost;
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveKey) == false)
            {
                SaveMoney(_balance);
            }

            _balance = LoadMoney();
            MoneyChanged?.Invoke(Balance);
        }

        public int Balance => _balance;

        public void AddMoney(int value)
        {
            _balance += value;
            SaveMoney(_balance);
            MoneyChanged?.Invoke(Balance);
        }

        public void SubMoney(int value)
        {
            _balance -= value;
            SaveMoney(_balance);
            MoneyChanged?.Invoke(Balance);
        }

        public bool HasMoney(int value)
        {
            if (_balance >= value)
            {
                return true;
            }

            return false;
        }

        public void ResetBalance()
        {
            PlayerPrefs.DeleteKey(SaveKey);
        }

        private void OnGameWon()
        {
            AddMoneyFromScore();
        }

        private void OnGameLost()
        {
            AddMoneyFromScore();
        }

        private void AddMoneyFromScore()
        {
            AddMoney(_player.Score);
        }

        private void SaveMoney(int value)
        {
            PlayerPrefs.SetInt(SaveKey, value);
        }

        private int LoadMoney()
        {
            return PlayerPrefs.GetInt(SaveKey);
        }
    }
}
