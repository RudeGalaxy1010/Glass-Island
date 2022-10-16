using TMPro;
using UnityEngine;

namespace GlassIsland.UI
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private SpeedUpgrade _speedUpgrade;
        [SerializeField] private UnlockHatPanel _unlockHatPanel;
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private Money _money;

        private void OnEnable()
        {
            _speedUpgrade.Updated += UpdateView;
            _unlockHatPanel.HatUlocked += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _speedUpgrade.Updated -= UpdateView;
            _unlockHatPanel.HatUlocked -= UpdateView;
        }

        private void UpdateView()
        {
            _balanceText.text = _money.Balance.ToString();
        }
    }
}
