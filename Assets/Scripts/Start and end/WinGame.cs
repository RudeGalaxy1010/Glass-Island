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

        private void Start()
        {
            Win();
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

            foreach (var bot in _bots)
            {
                bot.DisableOutline();
            }

            _winPanel.SetActive(true);
        }
    }
}
