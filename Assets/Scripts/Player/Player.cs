using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _storagePoint;

    private List<Brick> _bricks = new List<Brick>();

    public Transform StoragePoint => _storagePoint;

    public bool TryAddBrick(Brick brick)
    {
        if (_bricks.Contains(brick) == true)
        {
            return false;
        }

        _bricks.Add(brick);

        return true;
    }
}
