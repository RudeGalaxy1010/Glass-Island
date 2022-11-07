using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GlassIsland
{
    public class StartGame : MonoBehaviour
    {
        private const float _halfCircleInDegrees = 180f;

        [SerializeField] private LevelManager _levelManager;
        [SerializeField] Move[] _players;
        [SerializeField] private float _radius;
        [SerializeField] private float _startDelay;
        [SerializeField] private TileButton _tilePrefab;
        [SerializeField] private Button _startButton;

        private float _objectsCounter;

        private float _angleOffset => _halfCircleInDegrees / _players.Length;
        private float _spawnHeight => _levelManager.CurrentLevel.SpawnHeight;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void Start()
        {
            foreach (var player in _players)
            {
                player.GetComponent<Character>().DisableOutline();
                player.Disable();
                player.transform.position = GetRandomPosition();
                TileButton tile = Instantiate(_tilePrefab, player.transform.position + Vector3.down, Quaternion.identity);
                tile.ClearTile();
                tile.Pressed += (c) => Destroy(tile.gameObject, 3f);
            }
        }

        private void OnStartButtonClick()
        {
            StartCoroutine(ActivatePlayers(_startDelay));
        }

        private IEnumerator ActivatePlayers(float delay)
        {
            foreach (var player in _players)
            {
                player.GetComponent<Character>().EnableOutline();
            }

            yield return new WaitForSeconds(delay);

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
