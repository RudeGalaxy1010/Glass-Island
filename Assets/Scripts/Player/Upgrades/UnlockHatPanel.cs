using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlassIsland.UI
{
    public class UnlockHatPanel : MonoBehaviour
    {
        public const string Question = "Do you want to unlock this hat for {0}?";
        public event UnityAction HatUlocked;

        [SerializeField] private Image _hatImage;
        [SerializeField] private Button _approveButton;
        [SerializeField] private Button _declineButton;
        [SerializeField] private HatProperties _hatProperties;
        [SerializeField] private Money _money;
        [SerializeField] private TMP_Text _questionText;

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
            _questionText.text = Question.Replace("{0}", _hat.Cost.ToString());
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
