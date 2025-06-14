public class EXPItem : BaseItem
{
    protected override float speed => 50f;
    public override float size => 15f;
    public BaseEnemy enemyData;
    public override void ItemCollided()
    {
        base.ItemCollided();
        Game.playerEXPManager.AddEXP(enemyData);
        Game.DespawnItem(this);
    }
}