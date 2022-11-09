using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class AdsManager : MonoBehaviour
    {
        private bool _inited;

        public IEnumerator Start()
        {
            _inited = false;

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return YandexGamesSdk.Initialize();
            _inited = true;
        }

        public bool CanShowAds => _inited;

        public void ShowVideo(Action action)
        {
            if (_inited == false)
            {
                return;
            }

            VideoAd.Show(onRewardedCallback: () => action?.Invoke());
        }

        public void ShowInterstitial()
        {
            if (_inited == false)
            {
                return;
            }

            InterstitialAd.Show();
        }
    }
}
