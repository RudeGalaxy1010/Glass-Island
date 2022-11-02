using UnityEngine;

namespace GlassIsland
{
    public class DefaultTileButton : TileButton
    {
        private const int CoinValue = 5;
        private const int SingleBrickValue = 1;
        private const int BrickStackValue = 5;

        [SerializeField] private Dissolvable _body;
        [SerializeField] private BrickStack _bricks;
        [SerializeField] private Coin _coin;

        private const float _singleBrickChance = 0.25f;
        private const float _fiveBricksChance = 0.2f;
        private const float _coinChance = 0.15f;

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
            ClearTile();

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

        public override void ClearTile()
        {
            _bricks.gameObject.SetActive(false);
            _coin.gameObject.SetActive(false);
        }
    }
}
