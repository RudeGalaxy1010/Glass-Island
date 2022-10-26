using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlassIsland.UI
{
    [RequireComponent(typeof(Button))]
    public class HatButton : MonoBehaviour
    {
        public event UnityAction<HatButton> Clicked;

        [SerializeField] private GameObject _checkmark;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _lock;
        [SerializeField] private TMP_Text _costText;

        private Button _button;
        private Hat _hat;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Init(Hat hat)
        {
            _hat = hat;
            _image.sprite = hat.Sprite;
            
            if (_hat.IsUnlocked == false)
            {
                _costText.text = _hat.Cost.ToString();
            }
            else
            {
                _costText.gameObject.SetActive(false);
            }

            _lock.SetActive(!_hat.IsUnlocked);
        }

        public Hat Hat => _hat;

        public void SetSelectection()
        {
            _checkmark.SetActive(true);
        }

        public void ResetSelection()
        {
            _checkmark.SetActive(false);
        }

        private void OnClick()
        {
            Clicked?.Invoke(this);
        }
    }
}
