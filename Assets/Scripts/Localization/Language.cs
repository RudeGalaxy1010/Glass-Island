using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using Lean.Localization;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace GlassIsland.Localization
{
    public class Language : MonoBehaviour
    {
        private const string Key = "lang";
        private readonly List<string> LanguageNames = new List<string>() { "English", "Russian", "Turkish" };

        [SerializeField] private TMP_Dropdown _dropdown;

        private IEnumerator Start()
        {
            InitDropdown();

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (PlayerPrefs.GetInt(Key, 0) == 1)
            {
                yield break;
            }

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            string language = YandexGamesSdk.Environment.i18n.lang;
            int option = 0;

            switch(language)
            {
                case "en":
                case "En": option = 0;
                    break;
                case "ru":
                case "RU": option = 1;
                    break;
                case "tr":
                case "TR": option = 2;
                    break;
            }

            SelectLanguage(option);
            UpdateDropdown();
        }

        private void OnEnable()
        {
            _dropdown.onValueChanged.AddListener(SelectLanguage);
        }

        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveListener(SelectLanguage);
        }

        private void InitDropdown()
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(LanguageNames);
            _dropdown.value = LanguageNames.IndexOf(LeanLocalization.GetFirstCurrentLanguage());
            _dropdown.RefreshShownValue();
        }

        private void SelectLanguage(int option)
        {
            LeanLocalization.SetCurrentLanguageAll(LanguageNames[option]);
            PlayerPrefs.SetInt(Key, 1);
        }

        private void UpdateDropdown()
        {
            _dropdown.value = LanguageNames.IndexOf(LeanLocalization.GetFirstCurrentLanguage());
            _dropdown.RefreshShownValue();
        }
    }
}
