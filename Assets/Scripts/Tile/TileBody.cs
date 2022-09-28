using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GlassIsland
{
    [RequireComponent(typeof(Collider))]
    public class TileBody : MonoBehaviour
    {
        public event UnityAction Dissolved;
        public event UnityAction Pressed;
        public event UnityAction Unpressed;

        [SerializeField] private TileButton _tileButton;
        [SerializeField] private Dissolvable _dissolvingBody;
        [SerializeField] private List<Dissolvable> _dissolvableItems;
        
        [SerializeField] private float _extinctTime;
        [SerializeField] private float _extinctDelay;
        [SerializeField] private float _appearTime;
        [SerializeField] private bool _needFadeByFirstPress;

        private float _timer;
        private float _halfExtinctTime;
        private bool _needAppearing;
        private bool _needDissolving;

        public bool IsDissolved => _dissolvingBody.gameObject.activeSelf == false;

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
            _halfExtinctTime = _extinctTime / 2f;
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

        private float _appearAlphaValue => _timer / _appearTime;
        private float _dissolveAlphaValue => _timer / _extinctTime;
        private bool _isAppearTimerFinished => _timer / _appearTime > 1;
        private bool _isDissolveTimerFinished => _timer < -_halfExtinctTime;

        public void Press(Character character)
        {
            Pressed?.Invoke();

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
            _timer = _halfExtinctTime + _extinctDelay;
        }

        public void Unpress()
        {
            Unpressed?.Invoke();
        }

        private void Appear()
        {
            if (_isAppearTimerFinished == true)
            {
                _needAppearing = false;
                return;
            }

            _timer += Time.deltaTime;
            _dissolvingBody.SetAlpha(_appearAlphaValue);

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(_appearAlphaValue);
            }

            Pressed?.Invoke();
        }

        private void Dissolve()
        {
            _timer -= Time.deltaTime;
            _dissolvingBody.SetAlpha(_dissolveAlphaValue);

            foreach (var dissolvingItem in _dissolvableItems)
            {
                dissolvingItem.SetAlpha(_dissolveAlphaValue);
            }

            if (_isDissolveTimerFinished == true)
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
            Dissolved?.Invoke();
        }

        private void RemoveCollectable(Dissolvable dissolvable)
        {
            dissolvable.Dissolved -= RemoveCollectable;
            _dissolvableItems.Remove(dissolvable);
        }
    }
}
