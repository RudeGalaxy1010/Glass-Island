using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland
{
    public class LoseGame : MonoBehaviour
    {
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Player _player;
        [SerializeField] private List<Character> _bots;
        [SerializeField] private List<Move> _botMoves;
        [SerializeField] private SmoothFollow _smoothFollow;
        [SerializeField] private GameObject _joystick;

        private void OnEnable()
        {
            _player.Died += OnPlayerDied;

            foreach (var bot in _bots)
            {
                bot.Died += OnBotDied;
            }
        }

        private void OnDisable()
        {
            _player.Died -= OnPlayerDied;

            foreach (var bot in _bots)
            {
                bot.Died -= OnBotDied;
            }
        }

        private void OnBotDied(Character bot)
        {
            bot.Died -= OnBotDied;
            _bots.Remove(bot);
        }

        private void OnPlayerDied(Character player)
        {
            Lose();
        }

        private void Lose()
        {
            foreach (var bot in _bots)
            {
                bot.DisableOutline();
            }

            foreach (var bot in _botMoves)
            {
                bot.Disable();
            }

            _smoothFollow.SetDefaultTarget();
            _joystick.SetActive(false);
            _losePanel.SetActive(true);
        }
    }
}
