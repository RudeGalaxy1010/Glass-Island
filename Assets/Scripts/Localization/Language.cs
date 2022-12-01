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
        private readonly List<string> LanguageShortNames = new List<string>() { "en", "ru", "tr" };
        private readonly List<string> LanguageNames = new List<string>() { "English", "Russian", "Turkish" };

        [SerializeField] private TMP_Dropdown _dropdown;

        private IEnumerator Start()
        {
            InitDropdown();

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            int languageOption = PlayerPrefs.GetInt(Key, -1);

            if (languageOption > -1)
            {
                SelectLanguage(languageOption);
                yield break;
            }

            languageOption = GetDetectedLanguageOption();
            SelectLanguage(languageOption);
        }

        private int GetDetectedLanguageOption()
        {
            string language = YandexGamesSdk.Environment.i18n.lang;
            return LanguageShortNames.IndexOf(language);
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
