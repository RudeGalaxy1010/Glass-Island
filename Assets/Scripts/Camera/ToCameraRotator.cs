using UnityEngine;

public class ToCameraRotator : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }
}
