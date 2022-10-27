using UnityEngine;

namespace GlassIsland
{
    [CreateAssetMenu(fileName = "Player Properties", menuName = "Custom/Player Properties", order = 0)]
    public class PlayerProperties : ScriptableObject
    {
        public float Speed = 2.5f;
        public int SpeedCost = 25;
        public float MaxSpeed = 3f;

        [SerializeField] private float _speedUpgradeStep = 0.1f;
        [SerializeField] private int _speedCostStep = 5;

        public void UpgradeSpeed()
        {
            Speed += _speedUpgradeStep;
            SpeedCost += _speedCostStep;
        }
    }
}
