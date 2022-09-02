using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland
{
    public class LoseGame : MonoBehaviour
    {
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Player _player;
        [SerializeField] private List<Character> _bots;
        [SerializeField] private SmoothFollow _smoothFollow;

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
                bot.GetComponent<Move>().enabled = false;
            }

            _smoothFollow.SetDefaultTarget();
            _losePanel.SetActive(true);
        }
    }
}
