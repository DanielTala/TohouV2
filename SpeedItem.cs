using System.Numerics;

public class SpeedItem : BaseItem
{
    
    public float SpeedAdded = 20;

    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.player.AddSpeed(SpeedAdded);
        Game.DespawnItem(this);
    }


}