using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class Coin : Collectable
    {
        [SerializeField] private Animation _scaleAnimation;
        private int _value;
        private bool _isPickedUp;

        public override void Init(int value)
        {
            _value = value;
            _isPickedUp = false;
        }

        public override void PickUp(Player player)
        {
            if (_isPickedUp == true)
            {
                return;
            }

            _isPickedUp = true;
            _scaleAnimation.Play();
            StartCoroutine(Disable(player, _scaleAnimation.clip.length));
        }

        private IEnumerator Disable(Player player, float time)
        {
            yield return new WaitForSeconds(time);
            player.AddScore(_value);
            gameObject.SetActive(false);
        }
    }
}
