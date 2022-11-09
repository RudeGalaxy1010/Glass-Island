using UnityEngine;

namespace GlassIsland
{
    public class LevelLoseInterstitial : MonoBehaviour
    {
        [SerializeField] private AdsManager _adsManager;
        [SerializeField] private LoseGame _loseGame;

        private void OnEnable()
        {
            _loseGame.Lost += OnGameLost;
        }

        private void OnDisable()
        {
            _loseGame.Lost -= OnGameLost;
        }

        private void OnGameLost()
        {
            _adsManager.ShowInterstitial();
        }
    }
}
