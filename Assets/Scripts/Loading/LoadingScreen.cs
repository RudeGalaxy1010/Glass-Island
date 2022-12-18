using GlassIsland.Localization;
using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private Language _language;

        private void OnEnable()
        {
            _language.LanguageSet += OnLanguageSet;
        }

        private void OnDisable()
        {
            _language.LanguageSet -= OnLanguageSet;
        }

        private void OnLanguageSet()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            _animation.Play();
            yield return new WaitForSeconds(_animation.clip.length);
            gameObject.SetActive(false);
        }
    }
}
