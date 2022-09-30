using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlassIsland
{
    public class UpgradePanel : MonoBehaviour
    {
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
            UpdateUpgradeButton();
        }

        private void UpdateUpgradeButton()
        {
            _upgradeSpeedCostText.text = _playerProperties.SpeedCost.ToString();
            _upgradeSpeedButton.interactable = false;

            if (_money.HasMoney(_playerProperties.SpeedCost))
            {
                _upgradeSpeedButton.interactable = true;
            }
        }

        private void OnUpgradeSpeed()
        {
            _money.SubMoney(_playerProperties.SpeedCost);
            _playerProperties.UpgradeSpeed();
            UpdateUpgradeButton();
        }
    }
}
