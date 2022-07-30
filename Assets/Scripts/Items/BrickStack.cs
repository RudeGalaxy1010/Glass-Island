using UnityEngine;

public class BrickStack : Collectable
{
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;
    private int _bricksCount;

    public override void Init(int bricksCount)
    {
        _bricksCount = bricksCount;
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position == _targetPosition)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public override void PickUp(Player player)
    {
        player.AddBricks(_bricksCount);
        transform.SetParent(player.transform);
        _targetPosition = player.GetBrickPosition(this);
    }
}
