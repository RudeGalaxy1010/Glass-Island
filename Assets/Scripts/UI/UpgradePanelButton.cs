using UnityEngine;
using UnityEngine.UI;

namespace GlassIsland.UI
{
    public class UpgradePanelButton : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _upgradePanel;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnOpenButtonClick()
        {
            _upgradePanel.SetActive(true);
        }

        private void OnCloseButtonClick()
        {
            _upgradePanel.SetActive(false);
        }
    }
}
