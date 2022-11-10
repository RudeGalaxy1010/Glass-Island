using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using Lean.Localization;

namespace GlassIsland.Localization
{
    public class Language : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            string language = YandexGamesSdk.Environment.i18n.lang;
            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}
