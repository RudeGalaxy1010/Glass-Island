using UnityEngine;

namespace GlassIsland
{
    public class PlayerInput : InputBase
    {
        [SerializeField] private Joystick _joystick;

        private void Update()
        {
            InputVelocity = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        }
    }
}
