using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _storagePoint;

    private int _score;
    private int _bricksCount;

    public Vector3 GetBrickPosition(BrickStack brick)
    {
        return _storagePoint.position + Vector3.up * _bricksCount * brick.transform.lossyScale.y;
    }

    public void AddBricks(int value)
    {
        _bricksCount += value;
    }

    public void AddScore(int value)
    {
        _score += value;
    }
}
