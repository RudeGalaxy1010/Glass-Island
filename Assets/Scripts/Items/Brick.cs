using UnityEngine;

public class Brick : Collectable
{
    [SerializeField] private float _speed;

    private Transform _target;

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _target.position)
        {
            transform.SetParent(_target);
            _target = null;
        }
    }

    protected override void PickUp(Player player)
    {
        if (player.TryAddBrick(this) == true)
        {
            _target = player.StoragePoint;
        }
    }
}
