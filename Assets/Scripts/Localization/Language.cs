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
        private readonly List<string> LanguageNames = new List<string>() { "English", "Russian", "Turkish" };

        [SerializeField] private TMP_Dropdown _dropdown;

        private IEnumerator Start()
        {
            InitDropdown();

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            string language = YandexGamesSdk.Environment.i18n.lang;
            LeanLocalization.SetCurrentLanguageAll(language);
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
        }
    }
}
