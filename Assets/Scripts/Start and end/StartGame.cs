using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] PlayerMove[] _players;
        [SerializeField] private float _startHeight;
        [SerializeField] private float _spreading;
        [SerializeField] private float _startDelay;

        private void Start()
        {
            foreach (var player in _players)
            {
                player.enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                player.transform.position = GetRandomPosition();
            }

            StartCoroutine(EnablePlayers(_startDelay));
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 position = Random.insideUnitSphere * _spreading;
            return new Vector3(position.x, _startHeight, position.z);
        }

        private IEnumerator EnablePlayers(float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var player in _players)
            {
                player.enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
