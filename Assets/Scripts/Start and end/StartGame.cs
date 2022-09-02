using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] Move[] _players;
        [SerializeField] private float _startHeight;
        [SerializeField] private float _radius;
        [SerializeField] private float _startDelay;

        private float _objectsCounter;

        private float angleOffset => 180f / _players.Length;

        private void Start()
        {
            foreach (var player in _players)
            {
                player.Disable();
                player.transform.position = GetRandomPosition();
            }

            StartCoroutine(EnablePlayers(_startDelay));
        }

        private Vector3 GetRandomPosition()
        {
            float angle = _objectsCounter * Mathf.PI * 2f / _players.Length + angleOffset;
            _objectsCounter++;
            return transform.position + new Vector3(Mathf.Cos(angle) * _radius, 0, Mathf.Sin(angle) * _radius);
        }

        private IEnumerator EnablePlayers(float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var player in _players)
            {
                player.Enable();
            }
        }
    }
}
