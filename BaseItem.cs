using System.Numerics;
using Raylib_cs;

public class BaseItem
{
    protected virtual float speed { get => 10f; }
    protected Vector2 direction;
    protected Vector2 initialPosition;
    public float size = 30;
    public Vector2 position;

    public virtual void Initialize(Vector2 position)
    {
        var xDirection = Raylib.GetRandomValue(1, 100);
        var yDirection = Raylib.GetRandomValue(1, 100);
        direction = Raymath.Vector2Normalize(new Vector2(xDirection, yDirection));
        position = initialPosition;
    }

    public virtual void Update(float deltaTime)
    {
        position += direction * speed * deltaTime;

        if (position.X > Raylib.GetScreenWidth() - size / 2) // reflect to left
        {
            direction = Vector2.Reflect(direction, new Vector2(1, 0));
        }
        else if (position.X < size / 2) // reflect to right
        {
            direction = Vector2.Reflect(direction, new Vector2(-1, 0));
        }
        else if (position.Y > Raylib.GetScreenHeight() - size / 2) // reflect up
        {
            direction = Vector2.Reflect(direction, new Vector2(0, 1));
        }
        else if (position.Y < size / 2) // reflect down
        {
            direction = Vector2.Reflect(direction, new Vector2(0, -1));
        }
    }

    public virtual void Draw()
    {
        Raylib.DrawCircleLinesV(position, size, Color.Pink);
    }

    public virtual void ItemCollided()
    {
        
    }

}