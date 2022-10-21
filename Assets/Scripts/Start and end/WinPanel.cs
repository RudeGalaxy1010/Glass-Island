using UnityEngine;

namespace GlassIsland.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private Animation _animation;

        private void OnDisable()
        {
            _score.enabled = false;
            _animation.enabled = false;
        }
    }
}
