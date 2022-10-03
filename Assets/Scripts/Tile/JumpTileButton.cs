using System;
using System.Collections;
using UnityEngine;

namespace GlassIsland
{
    public class JumpTileButton : TileButton
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Dissolvable _dissolvable;

        protected override void Press(Character character)
        {
            base.Press(character);
            ThrowCharacter(character);
        }

        protected override void Unpress()
        {
            base.Unpress();
        }

        private void ThrowCharacter(Character character)
        {
            character.GetComponent<Move>().Jump(_jumpHeight);
            StartCoroutine(DeactivateTile());
        }

        private IEnumerator DeactivateTile()
        {
            yield return new WaitUntil(() => _dissolvable.IsDissolved == true);
            gameObject.SetActive(false);
        }
    }
}
