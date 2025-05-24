using System.Numerics;
using Raylib_cs;

public class ItemSpawner
{
    public static List<BaseItem> ItemList = new List<BaseItem>();
    public static List<BaseItem> ToRemove = new List<BaseItem>();
    public void Initialize()
    {

    }

    public void DespawnAllItems()
    {

    }
    public void Update(float deltaTime)
    {
        if (Raylib.IsKeyReleased(KeyboardKey.U))
        {
            var item = new BaseItem(500, new Vector2(100, 100));
            item.Initialize();
            ItemList.Add(item);
        }
        foreach (var item in ItemList)
        {
            item.Update(deltaTime);
        }
    }
    public void Draw()
    {
        foreach (var item in ItemList)
        {
            item.Draw();
        }
        Raylib.DrawText(ItemList.Count.ToString(), 100, 100, 10, Color.Green);
    }
}