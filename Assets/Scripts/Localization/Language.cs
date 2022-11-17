using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            int savedOption = PlayerPrefs.GetInt(Key, -1);

            if (savedOption > -1)
            {
                SelectLanguage(savedOption);
                yield break;
            }

            SelectDetectedLanguage();
        }

        private void SelectDetectedLanguage()
        {
            string language = YandexGamesSdk.Environment.i18n.lang;
            int option = 0;

            switch (language)
            {
                case "en":
                    option = 0;
                    break;
                case "ru":
                    option = 1;
                    break;
                case "tr":
                    option = 2;
                    break;
            }

            SelectLanguage(option);
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
            _dropdown.value = option;
            _dropdown.RefreshShownValue();

            PlayerPrefs.SetInt(Key, option);
        }
    }
}
