using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class AdsManager : MonoBehaviour
    {
        public IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            if (YandexGamesSdk.IsInitialized)
            {
                yield break;
            }

            yield return YandexGamesSdk.Initialize();
        }

        public void ShowVideo(Action action)
        {
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            VideoAd.Show(onRewardedCallback: () => action?.Invoke());
        }

        public void ShowInterstitial()
        {
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            InterstitialAd.Show();
        }
    }
}
