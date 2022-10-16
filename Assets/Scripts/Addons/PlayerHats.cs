using UnityEngine;

namespace GlassIsland
{
    public class PlayerHats : MonoBehaviour
    {
        [SerializeField] private HatProperties _hatProperties;
        [SerializeField] private GameObject[] _hats;

        private void Awake()
        {
            PickHat();
        }

        public void PickHat()
        {
            foreach (var hat in _hats)
            {
                hat.SetActive(false);
            }

            if (_hatProperties.CurrentHatId <= 0 || _hatProperties.CurrentHatId >= _hats.Length)
            {
                return;
            }

            _hats[_hatProperties.CurrentHatId - 1].SetActive(true);
        }
    }
}
