using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileButton : MonoBehaviour
    {
        public event UnityAction Pressed;
        public event UnityAction Unpressed;

        [SerializeField] private Collectable[] _collectables;

        private bool _isPressed;

        private void Start()
        {
            //float randomValue = Random.value;

            //if (randomValue <= 0.25f)
            //{
            //    foreach (var brick in _bricks)
            //    {
            //        brick.gameObject.SetActive(true);
            //    }

            //    _coin.gameObject.SetActive(false);
            //}
            //else if (randomValue <= 0.5f)
            //{
            //    foreach (var brick in _bricks)
            //    {
            //        brick.gameObject.SetActive(false);
            //    }

            //    _bricks[0].gameObject.SetActive(true);
            //    _coin.gameObject.SetActive(false);
            //}
            //else
            //{
            //    foreach (var brick in _bricks)
            //    {
            //        brick.gameObject.SetActive(false);
            //    }

            //    if (Random.value <= 0.25f)
            //    {
            //        _coin.gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        _coin.gameObject.SetActive(false);
            //    }
            //}
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (_isPressed == true)
                {
                    return;
                }

                _isPressed = true;

                foreach (var collectable in _collectables)
                {
                    collectable.PickUp(player);
                }

                Pressed?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _isPressed = false;
                Unpressed?.Invoke();
            }
        }
    }
}
