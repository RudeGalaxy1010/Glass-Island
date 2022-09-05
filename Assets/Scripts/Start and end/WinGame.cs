using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland
{
    public class WinGame : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Player _player;
        [SerializeField] private Move _playerMove;
        [SerializeField] private List<Character> _bots;
        [SerializeField] private GameObject _joystick;

        private void OnEnable()
        {
            foreach (var bot in _bots)
            {
                bot.Died += OnBotDied;
            }
        }

        private void OnDisable()
        {
            foreach (var bot in _bots)
            {
                bot.Died -= OnBotDied;
            }
        }

        private void OnBotDied(Character bot)
        {
            bot.Died -= OnBotDied;
            _bots.Remove(bot);

            if (_bots.Count == 0)
            {
                Win();
            }
        }

        private void Win()
        {
            _playerMove.Disable();
            _player.DisableOutline();
            _joystick.SetActive(false);

            foreach (var bot in _bots)
            {
                bot.DisableOutline();
            }

            _winPanel.SetActive(true);
        }
    }
}
