using System.Collections.Generic;
using UnityEngine;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileDissolve : MonoBehaviour
    {
        [SerializeField] private Dissolvable _dissolvingBody;
        [SerializeField] private List<Dissolvable> _dissolvableItems;

        [SerializeField] private float _dissolveTime;
        [SerializeField] private float _dissolveDelay;
        [SerializeField] private float _appearTime;
        [SerializeField] private bool _needFadeByFirstPress;

        private float _timer;
        private float _halfDissolveTime;
        private bool _needAppearing;
        private bool _needDissolving;

        private void OnEnable()
        {
            foreach (var dissolvingCollectable in _dissolvableItems)
            {
                dissolvingCollectable.Dissolved += RemoveCollectable;
            }
        }

        private void OnDisable()
        {
            foreach (var dissolvingCollectable in _dissolvableItems)
            {
                dissolvingCollectable.Dissolved -= RemoveCollectable;
            }
        }

        private void Start()
        {
            _halfDissolveTime = _dissolveTime / 2f;
        }

        private void Update()
        {
            if (_needAppearing == true)
            {
                Appear();
            }
            else if (_needDissolving == true)
            {
                Dissolve();
            }
        }

        public bool IsDissolved => _dissolvingBody.gameObject.activeSelf == false;
        private float AppearAlphaValue => _timer / _appearTime;
        private float DissolveAlphaValue => _timer / _dissolveTime;
        private bool IsAppearTimerFinished => _timer / _appearTime > 1;
        private bool IsDissolveTimerFinished => _timer < -_halfDissolveTime;

        public void OnPress(Character character)
        {
            if ((IsDissolved || _needDissolving) && character.TrySubtractBrick() == true)
            {
                gameObject.SetActive(true);
                _dissolvingBody.gameObject.SetActive(true);
                _needAppearing = true;
                _needDissolving = false;
                _timer = 0;

                if (_needFadeByFirstPress == false)
                {
                    return;
                }
            }

            _needDissolving = true;
            _timer = _halfDissolveTime + _dissolveDelay;
        }

        private void Appear()
        {
            if (IsAppearTimerFinished == true)
            {
                _needAppearing = false;
                return;
            }

            _timer += Time.deltaTime;
            _dissolvingBody.SetAlpha(AppearAlphaValue);

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(AppearAlphaValue);
            }
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;
            _dissolvingBody.SetAlpha(DissolveAlphaValue);

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(DissolveAlphaValue);
            }

            if (IsDissolveTimerFinished == true)
            {
                FinishDissolving();
            }
        }

        private void FinishDissolving()
        {
            int dissolvableCount = _dissolvableItems.Count;

            for (int i = 0; i < dissolvableCount; i++)
            {
                _dissolvableItems[i].FinishDissolving();

                if (dissolvableCount != _dissolvableItems.Count)
                {
                    dissolvableCount = _dissolvableItems.Count;
                    i--;
                }
            }

            _dissolvingBody.FinishDissolving();
            gameObject.SetActive(false);
            _needDissolving = false;
        }

        private void RemoveCollectable(Dissolvable dissolvable)
        {
            dissolvable.Dissolved -= RemoveCollectable;
            _dissolvableItems.Remove(dissolvable);
        }
    }
}
