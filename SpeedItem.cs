using System.Numerics;

public class SpeedItem : BaseItem
{

    protected override float speed => 500f;
    public float SpeedAdded = 20;


    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.player.AddSpeed(SpeedAdded);
        Game.DespawnItem(this);
    }


}