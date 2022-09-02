using UnityEngine;

namespace GlassIsland
{
    public abstract class InputBase : MonoBehaviour
    {
        protected Vector3 InputVelocity;

        public Vector3 Velocity => InputVelocity;
    }
}
