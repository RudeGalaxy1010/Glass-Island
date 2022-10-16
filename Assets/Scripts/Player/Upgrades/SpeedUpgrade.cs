using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlassIsland
{
    public class SpeedUpgrade : MonoBehaviour
    {
        private const float MaxSpeed = 8;

        public event UnityAction Updated;

        [SerializeField] private Button _upgradeSpeedButton;
        [SerializeField] private TMP_Text _upgradeSpeedCostText;
        [SerializeField] private PlayerProperties _playerProperties;
        [SerializeField] private Money _money;

        private void OnEnable()
        {
            _upgradeSpeedButton.onClick.AddListener(OnUpgradeSpeed);
        }

        private void OnDisable()
        {
            _upgradeSpeedButton.onClick.RemoveListener(OnUpgradeSpeed);
        }

        private void Start()
        {
            UpdateSpeedUpgrade();
        }

        private void UpdateSpeedUpgrade()
        {
            _upgradeSpeedCostText.text = _playerProperties.SpeedCost.ToString();
            _upgradeSpeedButton.interactable = false;

            if (_playerProperties.Speed >= MaxSpeed)
            {
                return;
            }

            if (_money.HasMoney(_playerProperties.SpeedCost))
            {
                _upgradeSpeedButton.interactable = true;
            }
        }

        private void OnUpgradeSpeed()
        {
            _money.SubMoney(_playerProperties.SpeedCost);
            _playerProperties.UpgradeSpeed();
            UpdateSpeedUpgrade();
            Updated?.Invoke();
        }
    }
}
