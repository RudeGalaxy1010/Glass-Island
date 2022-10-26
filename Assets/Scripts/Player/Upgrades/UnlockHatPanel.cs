using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlassIsland.UI
{
    public class UnlockHatPanel : MonoBehaviour
    {
        public event UnityAction HatUlocked;

        [SerializeField] private Image _hatImage;
        [SerializeField] private Button _approveButton;
        [SerializeField] private Button _declineButton;
        [SerializeField] private HatProperties _hatProperties;
        [SerializeField] private Money _money;

        private Hat _hat;

        private void OnEnable()
        {
            _approveButton.onClick.AddListener(OnApproveButtonClicked);
            _declineButton.onClick.AddListener(OnDeclineButtonClicked);
        }

        private void OnDisable()
        {
            _approveButton.onClick.RemoveListener(OnApproveButtonClicked);
            _declineButton.onClick.RemoveListener(OnDeclineButtonClicked);
        }

        public void Init(Hat hat)
        {
            _hat = hat;
            _hatImage.sprite = _hat.Sprite;
        }

        private void OnApproveButtonClicked()
        {
            _money.SubMoney(_hat.Cost);
            _hatProperties.UnlockHat(_hat.Id);
            HatUlocked?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnDeclineButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
