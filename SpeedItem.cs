using System.Numerics;

public class SpeedItem : BaseItem
{
    public float SpeedAdded = 20;
    public SpeedItem(float speed, Vector2 initialPosition) : base(speed, initialPosition)
    {

    }

    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.player.AddSpeed(SpeedAdded);
        Game.DespawnItem(this);
    }


}