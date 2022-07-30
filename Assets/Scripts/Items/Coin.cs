using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] private int _value;

    public override void PickUp(Player player)
    {
        player.AddScore(_value);
        gameObject.SetActive(false);
    }
}
