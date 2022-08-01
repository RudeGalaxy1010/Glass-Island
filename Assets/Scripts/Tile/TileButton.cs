using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileButton : MonoBehaviour
    {
        [SerializeField] private TileBody _body;
        [SerializeField] private BrickStack _bricks;
        [SerializeField] private Coin _coin;

        private const float _singleBrickChance = 0.25f;
        private const float _fiveBricksChance = 0.2f;
        private const float _coinChance = 0.15f;

        private bool _isPressed;

        private void Start()
        {
            if (_body.IsDissolved == false)
            {
                CreateItem();
            }
        }

        private void CreateItem()
        {
            float randomValue = Random.value;
            ClearCell();

            if (randomValue <= _coinChance)
            {
                _coin.gameObject.SetActive(true);
                _coin.Init(1);
            }
            else if (randomValue <= _coinChance + _singleBrickChance)
            {
                _bricks.gameObject.SetActive(true);
                _bricks.Init(1);
            }
            else if (randomValue <= _coinChance + _singleBrickChance + _fiveBricksChance)
            {
                _bricks.gameObject.SetActive(true);
                _bricks.Init(5);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Press(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Unpress();
            }
        }

        private void Press(Player player)
        {
            if (_isPressed == true)
            {
                return;
            }

            _isPressed = true;

            if (_bricks.gameObject.activeSelf)
            {
                _bricks.PickUp(player);
            }
            else if (_coin.gameObject.activeSelf)
            {
                _coin.PickUp(player);
            }

            _body.Press(player);
        }

        private void Unpress()
        {
            _isPressed = false;
            _body.Unpress();
        }

        private void ClearCell()
        {
            _bricks.gameObject.SetActive(false);
            _coin.gameObject.SetActive(false);
        }
    }
}
