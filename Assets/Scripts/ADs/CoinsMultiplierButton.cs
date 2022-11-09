using UnityEngine;
using UnityEngine.UI;

namespace GlassIsland
{
    [RequireComponent(typeof(Button))]
    public class CoinsMultiplierButton : MonoBehaviour
    {
        private const int Multiplier = 2;

        [SerializeField] private AdsManager _adsManager;
        [SerializeField] private Score _score;
        [SerializeField] private Money _money;
        [SerializeField] private Player _player;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.interactable = true;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _adsManager.ShowVideo(() => MultiplyScore());
        }

        private void MultiplyScore()
        {
            int scoreToAdd = _player.Score * (Multiplier - 1);
            _player.AddScore(scoreToAdd);
            _money.AddMoney(scoreToAdd);
            _score.UpdateScore();
            _button.interactable = false;
        }
    }
}
