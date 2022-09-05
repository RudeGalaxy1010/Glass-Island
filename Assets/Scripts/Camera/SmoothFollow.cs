using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private const float MinYDifference = 0.2f;

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 12.5f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _defaultTarget;

    private void Update()
    {
        SmoothMove();
    }

    public void SetDefaultTarget()
    {
        _target = _defaultTarget;
    }

    private void SmoothMove()
    {
        Vector3 desiredPosition = _target.position + _offset;
        float yDifferense = Mathf.Abs(transform.position.y - desiredPosition.y);

        if (yDifferense <= MinYDifference)
        {
            desiredPosition.y = transform.position.y;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.deltaTime);
    }
}
