using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland.UI
{
    public class HatsPanel : MonoBehaviour
    {
        [SerializeField] private HatButton _hatButtonPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private HatProperties _hatProperties;
        [SerializeField] private Money _money;
        [SerializeField] private UnlockHatPanel _unlockHatPanel;

        private List<HatButton> _hatButtons;
        private HatButton _selectedHatButton;

        private void Start()
        {
            _hatButtons = new List<HatButton>();
            _hatProperties.Load();
            CreateHats();
            UpdateHats();
        }

        private void OnEnable()
        {
            _unlockHatPanel.HatUlocked += OnUnlockHatPanelHatUnlocked;
        }

        private void OnDisable()
        {
            _unlockHatPanel.HatUlocked -= OnUnlockHatPanelHatUnlocked;
        }

        private void OnDestroy()
        {
            if (_hatButtons == null)
            {
                return;
            }

            for (int i = 0; i < _hatButtons.Count; i++)
            {
                _hatButtons[i].Clicked -= OnHatButtonClicked;
            }
        }

        private void CreateHats()
        {
            for (int i = 0; i < _hatProperties.Hats.Length; i++)
            {
                HatButton hatButton = Instantiate(_hatButtonPrefab, _container);
                hatButton.Init(_hatProperties.Hats[i]);
                hatButton.Clicked += OnHatButtonClicked;
                hatButton.ResetSelection();

                if (_hatProperties.CurrentHatId == _hatProperties.Hats[i].Id)
                {
                    hatButton.SetSelectection();
                }

                _hatButtons.Add(hatButton);
            }
        }

        private void OnHatButtonClicked(HatButton hatButton)
        {
            if (hatButton.Hat.IsUnlocked == false)
            {
                if (_money.HasMoney(hatButton.Hat.Cost) == false)
                {
                    return;
                }

                _unlockHatPanel.Init(hatButton.Hat);
                _unlockHatPanel.gameObject.SetActive(true);
                return;
            }

            for (int i = 0; i < _hatButtons.Count; i++)
            {
                _hatButtons[i].ResetSelection();
            }

            _selectedHatButton = hatButton;
            _selectedHatButton.SetSelectection();
            _hatProperties.SelectHat(_selectedHatButton.Hat.Id);
        }

        private void UpdateHats()
        {
            for (int i = 0; i < _hatButtons.Count; i++)
            {
                _hatButtons[i].Init(_hatProperties.Hats[i]);
            }
        }

        private void OnUnlockHatPanelHatUnlocked()
        {
            UpdateHats();
        }
    }
}
