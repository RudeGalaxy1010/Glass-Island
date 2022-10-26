using System;
using UnityEngine;

namespace GlassIsland
{
    public class Money : MonoBehaviour
    {
        private const string SaveKey = "Money";

        [SerializeField] private WinGame _winGame;
        [SerializeField] private Player _player;

        private int _balance;

        private void OnEnable()
        {
            _winGame.Won += UpdateMoney;
        }

        private void OnDisable()
        {
            _winGame.Won -= UpdateMoney;
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveKey) == false)
            {
                SaveMoney(10000);
            }

            _balance = LoadMoney();
        }

        public int Balance => _balance;

        public void AddMoney(int value)
        {
            _balance += value;
            SaveMoney(_balance);
        }

        public void SubMoney(int value)
        {
            _balance -= value;
            SaveMoney(_balance);
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

        private void UpdateMoney()
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
