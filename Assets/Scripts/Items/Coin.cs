using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class Coin : Collectable
    {
        [SerializeField] private Animation _animation;
        private int _value;

        public override void Init(int value)
        {
            _value = value;
        }

        public override void PickUp(Player player)
        {
            player.AddScore(_value);
            _animation.Play();
            StartCoroutine(Disable(_animation.clip.length));
        }

        private IEnumerator Disable(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}
