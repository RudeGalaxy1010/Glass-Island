using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileButton : MonoBehaviour
    {
        public UnityAction<Character> Pressed;
        public UnityAction Unpressed;

        private bool _isPressed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Press(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Unpress();
            }
        }

        protected virtual void Press(Character character)
        {
            if (_isPressed == true)
            {
                return;
            }

            _isPressed = true;
            Pressed?.Invoke(character);
        }

        protected virtual void Unpress()
        {
            _isPressed = false;
            Unpressed?.Invoke();
        }
    }
}
