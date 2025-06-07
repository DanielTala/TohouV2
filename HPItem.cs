using System.Numerics;

public class HPItem : BaseItem
{
    protected override float speed => 5f;
    public int HPAdded = 10;

    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.player.HP += HPAdded;
        Game.itemSpawner.DespawnItem(this);
    }
}