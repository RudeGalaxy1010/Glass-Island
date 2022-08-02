using UnityEngine;

namespace GlassIsland
{
    public class Coin : Collectable
    {
        [SerializeField] private float _movingSpeed;

        private int _value;
        private Transform _target;

        public override void Init(int value)
        {
            _value = value;
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.position, _movingSpeed * Time.deltaTime);

            if (transform.position == _target.position)
            {
                gameObject.SetActive(false);
            }
        }

        public override void PickUp(Player player)
        {
            player.AddScore(_value);
            transform.SetParent(null);
            _target = player.transform;
        }
    }
}
