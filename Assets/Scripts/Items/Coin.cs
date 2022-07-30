using UnityEngine;

public class Coin : Collectable
{
    private int _value;

    public override void Init(int value)
    {
        _value = value;
    }

    public override void PickUp(Player player)
    {
        player.AddScore(_value);
        gameObject.SetActive(false);
    }
}
