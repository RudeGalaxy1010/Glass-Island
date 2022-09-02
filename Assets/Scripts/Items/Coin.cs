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

        public override void PickUp(Character character)
        {
            if (_isPickedUp == true)
            {
                return;
            }

            _isPickedUp = true;
            _scaleAnimation.Play();
            StartCoroutine(Disable(character, _scaleAnimation.clip.length));
        }

        private IEnumerator Disable(Character character, float time)
        {
            yield return new WaitForSeconds(time);
            character.AddScore(_value);
            gameObject.SetActive(false);
        }
    }
}
