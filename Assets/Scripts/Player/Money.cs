using UnityEngine;

namespace GlassIsland
{
    public class Money : MonoBehaviour
    {
        private const string SaveKey = "Money";

        [SerializeField] private WinGame _winGame;
        [SerializeField] private Player _player;

        private int _money;

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
                SaveMoney(_money);
            }

            _money = LoadMoney();
        }

        private void UpdateMoney()
        {
            AddMoney(_player.Score);
        }

        public void AddMoney(int value)
        {
            _money += value;
            SaveMoney(_money);
        }

        public void SubMoney(int value)
        {
            _money -= value;
            SaveMoney(_money);
        }

        public bool HasMoney(int value)
        {
            if (_money >= value)
            {
                return true;
            }

            return false;
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
