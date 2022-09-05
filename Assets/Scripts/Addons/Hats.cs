using UnityEngine;

namespace GlassIsland
{
    public class Hats : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _hatChance;
        [SerializeField] private GameObject[] _hats;

        private void Awake()
        {
            PickRandomHat();
        }

        private void PickRandomHat()
        {
            if (Random.value > _hatChance)
            {
                return;
            }

            int hatIndex = Random.Range(0, _hats.Length);

            foreach (var hat in _hats)
            {
                hat.SetActive(false);
            }

            _hats[hatIndex].SetActive(true);
        }
    }
}
