using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileButton : MonoBehaviour
    {
        private const int CoinValue = 5;
        private const int SingleBrickValue = 1;
        private const int BrickStackValue = 5;

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
                _coin.Init(CoinValue);
            }
            else if (randomValue <= _coinChance + _singleBrickChance)
            {
                _bricks.gameObject.SetActive(true);
                _bricks.Init(SingleBrickValue);
            }
            else if (randomValue <= _coinChance + _singleBrickChance + _fiveBricksChance)
            {
                _bricks.gameObject.SetActive(true);
                _bricks.Init(BrickStackValue);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Press(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Unpress();
            }
        }

        private void Press(Character character)
        {
            if (_isPressed == true)
            {
                return;
            }

            _isPressed = true;
            _body.Press(character);
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
