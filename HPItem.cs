using System.Numerics;

public class HPItem : BaseItem
{
    public int HPAdded = 10;
    public HPItem(float speed, Vector2 initialPosition) : base(speed, initialPosition)
    {

    }

    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.player.HP += HPAdded;
        Game.itemSpawner.DespawnItem(this);
    }
}