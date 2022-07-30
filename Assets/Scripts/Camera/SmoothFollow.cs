using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 12.5f;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        SmoothMove();
    }

    private void SmoothMove()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.deltaTime);
    }
}
