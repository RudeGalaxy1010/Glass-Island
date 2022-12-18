using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland.Localization
{
    public class Language : MonoBehaviour
    {
        private readonly List<string> LanguageShortNames = new List<string>() { "en", "ru", "tr" };
        private readonly List<string> LanguageNames = new List<string>() { "English", "Russian", "Turkish" };

        public UnityAction LanguageSet;

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            int languageOption = GetDetectedLanguageOption();
            SelectLanguage(languageOption);
            LanguageSet?.Invoke();
        }

        private int GetDetectedLanguageOption()
        {
            string language = YandexGamesSdk.Environment.i18n.lang;
            return LanguageShortNames.IndexOf(language);
        }

        private void SelectLanguage(int option)
        {
            LeanLocalization.SetCurrentLanguageAll(LanguageNames[option]);
        }
    }
}
