using UnityEngine;
using UnityEngine.Events;

public class Dissolvable : MonoBehaviour
{
    public event UnityAction<Dissolvable> Dissolved;

    [SerializeField] private Renderer[] _renderers;

    private Material[] _materials;
    private float[] _maxAlphas;

    private void Start()
    {
        _materials = new Material[_renderers.Length];
        _maxAlphas = new float[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
        {
            _materials[i] = _renderers[i].material;
            _maxAlphas[i] = _materials[i].color.a;
        }
    }

    public bool IsDissolved => !gameObject.activeSelf;

    public void Appear()
    {
        gameObject.SetActive(true);

        if (_materials == null || _maxAlphas == null)
        {
            return;
        }

        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].color = new Color(_materials[i].color.r, _materials[i].color.g, _materials[i].color.b, _maxAlphas[i]);
        }
    }

    public void SetAlpha(float alpha)
    {
        if (_materials == null || _maxAlphas == null)
        {
            return;
        }

        for (int i = 0; i < _materials.Length; i++)
        {
            alpha = Mathf.Min(alpha, _maxAlphas[i]);
            _materials[i].color = new Color(_materials[i].color.r, _materials[i].color.g, _materials[i].color.b, alpha);
        }
    }

    public void FinishDissolving(bool isPickedUp = false)
    {
        if (isPickedUp == false)
        {
            gameObject.SetActive(false);
        }

        Dissolved?.Invoke(this);
    }
}
