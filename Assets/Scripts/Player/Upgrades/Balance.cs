using TMPro;
using UnityEngine;

namespace GlassIsland.UI
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private Money _money;

        private void OnEnable()
        {
            _money.MoneyChanged += OnMoneyChanged;
            OnMoneyChanged(_money.Balance);
        }

        private void OnDisable()
        {
            _money.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _balanceText.text = money.ToString();
        }
    }
}
