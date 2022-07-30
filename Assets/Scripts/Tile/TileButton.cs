using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileButton : MonoBehaviour
    {
        public event UnityAction Pressed;
        public event UnityAction Unpressed;

        private bool _isPressed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (_isPressed == true)
                {
                    return;
                }

                _isPressed = true;
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
