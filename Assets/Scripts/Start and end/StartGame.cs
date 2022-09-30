using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class StartGame : MonoBehaviour
    {
        private const float _halfCircleInDegrees = 180f;

        [SerializeField] private LevelManager _levelManager;
        [SerializeField] Move[] _players;
        [SerializeField] private float _radius;
        [SerializeField] private float _startDelay;

        private float _objectsCounter;

        private float _angleOffset => _halfCircleInDegrees / _players.Length;
        private float _spawnHeight => _levelManager.CurrentLevel.SpawnHeight;

        private IEnumerator Start()
        {
            foreach (var player in _players)
            {
                player.Disable();
                player.transform.position = GetRandomPosition();
            }

            yield return new WaitForSeconds(_startDelay);

            foreach (var player in _players)
            {
                player.Enable();
            }
        }

        private Vector3 GetRandomPosition()
        {
            float angle = (_objectsCounter * Mathf.PI * 2f / _players.Length) + _angleOffset;
            _objectsCounter++;

            return new Vector3(Mathf.Cos(angle) * _radius, _spawnHeight, Mathf.Sin(angle) * _radius);
        }
    }
}
